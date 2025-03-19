import { UseFormReturnType } from '@mantine/form';

export type _TransformValues<Values> = (values: Values) => unknown;
export type KeyOfType<T, V> = {
  [K in keyof T]: T[K] extends V ? K : never;
}[keyof T];

export interface FormInputProps<
  Value,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> {
  form: Pick<UseFormReturnType<Values, TransformValues>, 'getInputProps'>;
  field: KeyOfType<Values, Value>;
  required?: boolean;
}
