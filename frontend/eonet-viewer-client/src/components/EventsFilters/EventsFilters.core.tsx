import { Category, Source } from '../../clients';
import { SelectTableData, SelectTableDataRow } from '../SelectTable';
import { useEffect, useMemo, useState } from 'react';

import { ComboboxItem } from '@mantine/core';
import { DatesRangeValue } from '@mantine/dates';
import { ExternalLinkIconButton } from '../ExternalLinkIconButton';
import { FormModalTableMultiSelectLabels } from '../form';
import { useEventsContextState } from '../../state';
import { useForm } from '@mantine/form';

export type EventsFiltersFormStatus = 'open' | 'closed' | 'all';

export interface EventsFiltersFormValues {
  categories: string[];
  sources: string[];
  status: EventsFiltersFormStatus;
  dates: DatesRangeValue;
}

export const useCategoriesData = (categories: Category[]) => {
  return useMemo(
    () => categories.map((category): ComboboxItem => ({ value: category.id, label: category.title })),
    [categories],
  );
};

export const useSourcesData = (sources: Source[]): SelectTableData =>
  useMemo(
    () => ({
      head: ['Id', 'Title', 'Url'],
      body: sources.map(
        (source): SelectTableDataRow => ({
          value: source.id,
          cells: [
            source.id,
            source.title,
            <ExternalLinkIconButton key="url" href={source.sourceUrl} title={source.title} />,
          ],
        }),
      ),
    }),
    [sources],
  );

export const sourcesLabels: FormModalTableMultiSelectLabels = {
  summary: (total: number, count: number) =>
    count == total ? `All sources included` : `${count.toString()}/${total.toString()} sources included`,
};

export const useStatusData = (): ComboboxItem[] =>
  useMemo(
    () => [
      { value: 'open', label: 'Open' },
      { value: 'closed', label: 'Closed' },
      { value: 'all', label: 'All (Open & Closed)' },
    ],
    [],
  );

export const useEventsFiltersForm = (values?: Partial<EventsFiltersFormValues>) => {
  const { categories, sources, isLoading: isContextLoading } = useEventsContextState();
  const form = useForm<EventsFiltersFormValues, (values: EventsFiltersFormValues) => Partial<EventsFiltersFormValues>>(
    useMemo(() => {
      const initialValues: EventsFiltersFormValues = {
        categories: categories.keys,
        sources: sources.keys,
        status: 'open',
        dates: [null, null],
      };

      return {
        initialValues,
        validate: {
          categories: (value) => (value.length > 0 ? null : 'At least one category is required'),
          sources: (value) => (value.length > 0 ? null : 'At least one source is required'),
        },
        transformValues: (values): Partial<EventsFiltersFormValues> => ({
          ...values,
          categories: values.categories.length === categories.keys.length ? undefined : values.categories,
          sources: values.sources.length === sources.keys.length ? undefined : values.sources,
        }),
      };
    }, [categories, sources]),
  );

  const { setValues, resetDirty } = form;
  const [initializedWithContext, setInitializedWithContext] = useState(false);
  useEffect(() => {
    if (!initializedWithContext && !isContextLoading) {
      setInitializedWithContext(true);
      setValues((v) => ({ ...v, categories: categories.keys, sources: sources.keys }));
    }
  }, [categories, sources, isContextLoading, initializedWithContext, setValues]);

  useEffect(() => {
    if (values) {
      const valuesMergedWithDefaults: EventsFiltersFormValues = {
        categories: values.categories ?? categories.keys,
        sources: values.sources ?? sources.keys,
        status: values.status ?? 'open',
        dates: values.dates ?? [null, null],
      };

      setValues(valuesMergedWithDefaults);
      resetDirty();
    }
  }, [values, setValues, resetDirty, categories, sources]);

  return {
    ...form,
    categoriesData: useCategoriesData(categories.values),
    sourcesData: useSourcesData(sources.values),
    statusData: useStatusData(),
  };
};
