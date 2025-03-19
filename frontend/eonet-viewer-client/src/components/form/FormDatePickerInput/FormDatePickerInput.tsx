import { DatePickerInput, DatePickerInputProps, DatePickerType, DatePickerValue } from '@mantine/dates';
import { FormInputProps, _TransformValues } from '../form';
import { dateFormat, datePlaceholder, dateRangePlaceholder } from '../../../helpers';

import { IconCalendar } from '@tabler/icons-react';
import { useMemo } from 'react';

interface FormDatePickerInputProps<
  Type extends DatePickerType = 'default',
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> extends Omit<DatePickerInputProps<Type>, 'form' | 'minDate'>,
    FormInputProps<DatePickerValue<Type>, Values, TransformValues> {
  minDate?: Date | null;
}

const getDatePickerPlaceholder = (type?: DatePickerType): string => {
  switch (type) {
    case 'range':
      return dateRangePlaceholder;
    case 'default':
    case 'multiple':
    default:
      return datePlaceholder;
  }
};

export function FormDatePickerInput<
  Type extends DatePickerType = 'default',
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
>({ form, field, required, minDate, ...props }: FormDatePickerInputProps<Type, Values, TransformValues>) {
  const inputProps = useMemo(() => form.getInputProps(field), [form, field]);

  return (
    <DatePickerInput
      {...inputProps}
      valueFormat={dateFormat}
      placeholder={getDatePickerPlaceholder(props.type)}
      leftSection={<IconCalendar />}
      withAsterisk={!!required}
      clearable={!required}
      minDate={minDate ?? undefined}
      {...props}
    />
  );
}
