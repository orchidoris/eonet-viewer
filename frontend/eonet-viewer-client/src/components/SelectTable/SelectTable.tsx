import { Checkbox, Table, TableData, TableProps } from '@mantine/core';
import { useCallback, useMemo } from 'react';

export interface SelectTableData extends Omit<TableData, 'body'> {
  body?: SelectTableDataRow[];
}

export interface SelectTableDataRow {
  value: string;
  cells: React.ReactNode[];
}

interface SelectTableProps extends Omit<TableProps, 'data' | 'onChange'> {
  value: string[];
  onChange: (value: string[]) => void;
  data: SelectTableData;
}

export function SelectTable({ value = [], onChange, data: { head, body = [], ...data }, ...props }: SelectTableProps) {
  const onHeaderCheck = useCallback(() => {
    onChange(value.length === body.length ? [] : body.map((row) => row.value));
  }, [body, onChange, value.length]);

  const onRowCheck = useCallback(
    (rowValue: string): (() => void) => {
      return () => {
        onChange(value.includes(rowValue) ? value.filter((item) => item !== rowValue) : [...value, rowValue]);
      };
    },
    [onChange, value],
  );

  const tableData: TableData = useMemo(() => {
    return {
      ...data,
      head: head
        ? [
            <Checkbox
              key="checkbox"
              checked={value.length === body.length}
              indeterminate={value.length > 0 && value.length < body.length}
              onChange={onHeaderCheck}
            />,
            ...head,
          ]
        : undefined,
      body: body.map((row) => [
        <Checkbox key="checkbox" checked={value.includes(row.value)} onChange={onRowCheck(row.value)} />,
        ...row.cells,
      ]),
    };
  }, [body, data, head, onHeaderCheck, onRowCheck, value]);

  return <Table data={tableData} {...props} />;
}
