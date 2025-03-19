import { Button, Group, Stack, Text } from '@mantine/core';
import { EventsFiltersFormValues, sourcesLabels, useEventsFiltersForm } from './EventsFilters.core';
import { FormDatePickerInput, FormModalTableMultiSelect, FormMultiSelect, FormSelect } from '../form';

import { EventCategoryIcon } from '../EventCategoryIcon/EventCategoryIcon';
import { EventCategoryId } from '../../clients';
import { Form } from '@mantine/form';
import { IconCheck } from '@tabler/icons-react';
import classes from './EventsFilters.module.css';
import { getTodayUtcDate } from '../../helpers';

interface EventsFiltersProps extends Omit<React.ComponentPropsWithRef<'form'>, 'onSubmit'> {
  onSubmit?: (values: Partial<EventsFiltersFormValues>) => void;
  value?: Partial<EventsFiltersFormValues>;
}

export function EventsFilters({ value, ...props }: EventsFiltersProps) {
  const { categoriesData, sourcesData, statusData, ...form } = useEventsFiltersForm(value);
  const { isDirty: getIsDirty, reset } = form;
  const isDirty = getIsDirty();
  return (
    <Form form={form} {...props}>
      <Stack>
        <FormMultiSelect
          form={form}
          field="categories"
          required
          clearable
          label="Categories"
          data={categoriesData}
          withCheckIcon
          renderOption={(c) => (
            <>
              <IconCheck className={classes.check} {...(c.checked ? { ['data-checked']: '' } : {})} />
              <EventCategoryIcon
                className={classes.categorySelectOptionIcon}
                categoryId={c.option.value as EventCategoryId}
              />
              <span>{c.option.label}</span>
            </>
          )}
        />
        <FormModalTableMultiSelect
          form={form}
          field="sources"
          label="Sources"
          required
          labels={sourcesLabels}
          data={sourcesData}
        />
        <FormSelect
          form={form}
          field="status"
          label="Status"
          data={statusData}
          required
          renderOption={(s) => (
            <>
              <IconCheck className={classes.check} {...(s.checked ? { ['data-checked']: '' } : {})} />
              <span>{s.option.label}</span>
            </>
          )}
        />
        <FormDatePickerInput
          form={form}
          field="dates"
          type="range"
          allowSingleDateInRange
          label="Dates Range"
          maxDate={getTodayUtcDate()}
        />
        <Stack gap="xs">
          <Group justify="flex-end">
            {isDirty && (
              <Button variant="default" onClick={reset}>
                Reset
              </Button>
            )}
            <Button
              type="submit"
              disabled={!isDirty}
              title={isDirty ? 'Apply filters' : 'Current filters are already applied'}
            >
              Submit
            </Button>
          </Group>
          <Group justify="flex-end">
            {isDirty && (
              <Text size="xs" c="dimmed">
                Filters will be applied after "Submit" click
              </Text>
            )}
          </Group>
        </Stack>
      </Stack>
    </Form>
  );
}
