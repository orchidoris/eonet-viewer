import { Checkbox, Table, TableData, TableProps } from '@mantine/core';

import { useMemo } from 'react';

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
  const tableData: TableData = useMemo(() => {
    const toggleRow = (rowValue: string) => {
      onChange(value.includes(rowValue) ? value.filter((item) => item !== rowValue) : [...value, rowValue]);
    };

    return {
      ...data,
      head: head
        ? [
            <Checkbox
              checked={value.length === body.length}
              indeterminate={value.length > 0 && value.length < body.length}
              onChange={() => onChange(value.length === body.length ? [] : body.map((row) => row.value))}
            />,
            ...head,
          ]
        : undefined,
      body: body?.map((row) => [
        <Checkbox checked={value.includes(row.value)} onChange={() => toggleRow(row.value)} />,
        ...row.cells,
      ]),
    };
  }, [body, data, head, onChange, value]);

  return <Table data={tableData} {...props} />;
}
