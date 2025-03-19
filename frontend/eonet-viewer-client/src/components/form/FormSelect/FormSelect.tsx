import { FormInputProps, _TransformValues } from '../form';
import { Select, SelectProps } from '@mantine/core';

import { useMemo } from 'react';

interface FormSelectProps<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> extends Omit<SelectProps, 'form'>,
    FormInputProps<string | null | undefined, Values, TransformValues> {}

export function FormSelect<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
>({ form, field, required, ...props }: FormSelectProps<Values, TransformValues>) {
  const inputProps = useMemo(() => form.getInputProps(field), [form, field]);
  return (
    <Select
      {...inputProps}
      allowDeselect={!required}
      withAsterisk={!!required}
      clearable={!required}
      placeholder={`Select ${props.label}`}
      searchable
      {...props}
    />
  );
}
