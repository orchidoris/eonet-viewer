import { ElementProps, Input, InputBase, Modal, __BaseInputProps } from '@mantine/core';
import { FormInputProps, _TransformValues } from '../form';
import { IconPencil, IconSquareCheck } from '@tabler/icons-react';
import { SelectTable, SelectTableData } from '../../SelectTable';
import { useDisclosure, useUncontrolled } from '@mantine/hooks';

import classes from './FormModalTableMultiSelect.module.css';
import { cx } from '../../../helpers';
import { useCallback } from 'react';

export interface FormModalTableMultiSelectLabels {
  label?: string;
  modalTitle?: string;
  placeholder?: string;
  summary?: (valueCount: number, optionsCount: number) => string;
}

export interface FormModalTableMultiSelectProps<
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  Values extends Record<string, any> = Record<string, any>,
  TransformValues extends _TransformValues<Values> = (values: Values) => Values,
> extends Omit<__BaseInputProps, 'form'>,
    ElementProps<'button', 'form'>,
    FormInputProps<string[], Values, TransformValues> {
  data?: SelectTableData;
  clearable?: boolean;
  readOnly?: boolean;
  selectAllButtonProps?: React.ComponentPropsWithoutRef<'button'>;
  labels?: FormModalTableMultiSelectLabels;
}

export interface InputProps<T> {
  onChange: (value: T) => void;
  value?: T;
  defaultValue?: T;
  checked?: boolean;
  error?: React.ReactNode;
  onFocus?: React.FocusEventHandler | undefined;
  onBlur?: React.FocusEventHandler | undefined;
}

export const FormModalTableMultiSelect = ({
  form,
  field,
  data = {},
  labels = {},
  rightSection,
  readOnly,
  label,
  ...props
}: FormModalTableMultiSelectProps) => {
  const [opened, { open, close }] = useDisclosure(false);

  const { onChange, value, defaultValue, error, onFocus, onBlur } = form.getInputProps(field) as InputProps<string[]>;
  const optionsCount = data.body?.length ?? 0;
  const valueCount = value?.length ?? 0;
  const allSelected = optionsCount == valueCount;

  const [_value, setValue] = useUncontrolled({
    value,
    defaultValue,
    finalValue: [],
    onChange,
  });

  const onCheckAll = useCallback(() => {
    setValue(data.body?.map((row) => row.value) ?? []);
  }, [data.body, setValue]);

  return (
    <>
      <Modal opened={opened} onClose={close} title={labels.modalTitle ?? label ?? 'Options'} size="lg">
        <SelectTable value={_value} onChange={onChange} data={data} stickyHeader={true} />
      </Modal>
      <InputBase
        label={label}
        component={'button'}
        pointer
        onFocus={onFocus}
        onBlur={onBlur}
        error={error}
        onClick={open}
        rightSectionWidth={allSelected ? '28px' : '50px'}
        rightSection={
          rightSection ??
          (!readOnly && (
            <>
              <IconPencil className={cx(classes.icon)} stroke={0.8} size="1.2rem" onClick={open} />
              {!allSelected && (
                <IconSquareCheck
                  title="Include all options"
                  className={cx(classes.icon, classes.iconSquareCheck)}
                  stroke={0.8}
                  size="1.2rem"
                  onClick={onCheckAll}
                />
              )}
            </>
          ))
        }
        {...props}
      >
        {!valueCount ? (
          <Input.Placeholder>{labels.placeholder ?? 'Select options'}</Input.Placeholder>
        ) : (
          <div style={{ overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
            {labels.summary
              ? labels.summary(optionsCount, valueCount)
              : allSelected
                ? 'All options included'
                : `${valueCount.toString()}/${optionsCount.toString()} options included`}
          </div>
        )}
      </InputBase>
    </>
  );
};
