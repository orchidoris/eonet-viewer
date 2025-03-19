import { EventsContextCategories, EventsContextSources, useEventsContext } from '../../contexts';
import { SelectTableData, SelectTableDataRow } from '../SelectTable';
import { useEffect, useMemo, useState } from 'react';

import { ComboboxItem } from '@mantine/core';
import { DatesRangeValue } from '@mantine/dates';
import { ExternalLinkIconButton } from '../ExternalLinkIconButton';
import { FormModalTableMultiSelectLabels } from '../form';
import { useForm } from '@mantine/form';

export type EventsFiltersFormStatus = 'open' | 'closed' | 'all';

export interface EventsFiltersFormValues {
  categories: string[];
  sources: string[];
  status: EventsFiltersFormStatus;
  dates: DatesRangeValue;
}

export const useCategoriesData = (categories: EventsContextCategories) => {
  return useMemo(
    () =>
      Object.entries(categories).map(
        ([categoryKey, category]): ComboboxItem => ({
          value: categoryKey,
          label: category.title,
        }),
      ) ?? [],
    [categories],
  );
};

export const useSourcesData = (sources: EventsContextSources): SelectTableData =>
  useMemo(
    () => ({
      head: ['Id', 'Title', 'Url'],
      body:
        Object.entries(sources).map(
          ([sourceKey, source]): SelectTableDataRow => ({
            value: sourceKey,
            cells: [source.id, source.title, <ExternalLinkIconButton href={source.sourceUrl} title={source.title} />],
          }),
        ) ?? [],
    }),
    [sources],
  );

export const sourcesLabels: FormModalTableMultiSelectLabels = {
  summary: (total: number, count: number) =>
    count == total ? `All sources included` : `${count}/${total} sources included`,
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
  const { categories, sources, isLoading: isContextLoading } = useEventsContext();
  const form = useForm<EventsFiltersFormValues, (values: EventsFiltersFormValues) => Partial<EventsFiltersFormValues>>(
    useMemo(() => {
      const initialValues: EventsFiltersFormValues = {
        categories: Object.keys(categories),
        sources: Object.keys(sources),
        status: 'open',
        dates: [null, null],
      };

      return {
        initialValues,
        validate: {
          categories: (value) => (value && value.length > 0 ? null : 'At least one category is required'),
          sources: (value) => (value && value.length > 0 ? null : 'At least one source is required'),
        },
        transformValues: (values): Partial<EventsFiltersFormValues> => ({
          ...values,
          categories: values.categories.length === Object.keys(categories).length ? undefined : values.categories,
          sources: values.sources.length === Object.keys(sources).length ? undefined : values.sources,
        }),
      };
    }, [categories, sources]),
  );

  const { setValues, resetDirty } = form;
  const [initialized, setInitialized] = useState(false);
  useEffect(() => {
    if (!initialized && !isContextLoading) {
      setInitialized(true);
      setValues((v) => ({ ...v, categories: Object.keys(categories), sources: Object.keys(sources) }));
    }
  }, [categories, sources, isContextLoading, initialized, setValues]);

  useEffect(() => {
    if (values) {
      const valuesMergedWithDefaults: EventsFiltersFormValues = {
        categories: values.categories ?? Object.keys(categories),
        sources: values.sources ?? Object.keys(sources),
        status: values.status ?? 'open',
        dates: values.dates ?? [null, null],
      };

      setValues(valuesMergedWithDefaults);
      resetDirty();
    }
  }, [values, setValues, resetDirty, categories, sources]);

  return {
    ...form,
    categoriesData: useCategoriesData(categories),
    sourcesData: useSourcesData(sources),
    statusData: useStatusData(),
  };
};
