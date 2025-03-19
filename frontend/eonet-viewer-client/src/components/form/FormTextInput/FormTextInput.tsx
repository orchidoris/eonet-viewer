import { FormInputProps, _TransformValues } from '../form';
import { TextInput, TextInputProps } from '@mantine/core';

import { useMemo } from 'react';

interface FormTextInputProps<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> extends Omit<TextInputProps, 'form'>,
    FormInputProps<string | number | readonly string[] | undefined, Values, TransformValues> {}

export function FormTextInput<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
>({ form, field, required, ...props }: FormTextInputProps<Values, TransformValues>) {
  const inputProps = useMemo(() => form.getInputProps(field), [form, field]);
  return <TextInput {...inputProps} withAsterisk={!!required} placeholder={`Enter ${props.label}`} {...props} />;
}
