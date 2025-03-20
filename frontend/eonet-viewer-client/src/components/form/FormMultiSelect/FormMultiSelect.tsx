import { FormInputProps, _TransformValues } from '../form';
import { MultiSelect, MultiSelectProps, RemoveScroll } from '@mantine/core';

import { useDisclosure } from '@mantine/hooks';
import { useMemo } from 'react';

interface FormMultiSelectProps<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> extends Omit<MultiSelectProps, 'form'>,
    FormInputProps<string[] | undefined, Values, TransformValues> {}

export function FormMultiSelect<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
>({ form, field, required, onDropdownOpen, onDropdownClose, ...props }: FormMultiSelectProps<Values, TransformValues>) {
  const inputProps = useMemo(() => form.getInputProps(field), [form, field]);
  const [scrollLocked, { open: lockScroll, close: unlockScroll }] = useDisclosure();

  return (
    <RemoveScroll enabled={scrollLocked}>
      <MultiSelect
        {...inputProps}
        withAsterisk={!!required}
        clearable={!required}
        placeholder={typeof props.label === 'string' ? `Enter ${props.label}` : undefined}
        onDropdownOpen={() => {
          lockScroll();
          if (onDropdownOpen) onDropdownOpen();
        }}
        onDropdownClose={() => {
          unlockScroll();
          if (onDropdownClose) onDropdownClose();
        }}
        {...props}
      />
    </RemoveScroll>
  );
}
