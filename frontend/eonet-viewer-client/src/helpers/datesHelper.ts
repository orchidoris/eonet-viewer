import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';

export const dateRangePlaceholder = '____-__-__ - ____-__-__';
export const datePlaceholder = '____-__-__';
export const dateTimePlaceholder = '____-__-__ __:__';

export const dateFormat = 'YYYY-MM-DD';
export const dateTimeFormat = 'YYYY-MM-DD HH:mm';

dayjs.extend(utc);

export const getDateString = (dateString: string) => {
  if (!dateString) return '';

  const date: Date = new Date(dateString);
  if (Number.isNaN(date.getTime())) return '';
  return dayjs(date).format(dateFormat);
};

export const getDateTimeString = (dateString: string) => {
  if (!dateString) return '';

  const date: Date = new Date(dateString);
  if (Number.isNaN(date.getTime())) return '';

  return dayjs(date).format(dateTimeFormat);
};

export const getMinDate = (date1?: Date | null, date2?: Date | null): Date | undefined => {
  if (!date1) return date2 ?? undefined;
  if (!date2) return date1;

  return date1 < date2 ? date1 : date2;
};

export const getMaxDate = (date1?: Date, date2?: Date): Date | undefined => {
  if (!date1) return date2;
  if (!date2) return date1;

  return date1 > date2 ? date1 : date2;
};

export const getTodayUtc = () => dayjs.utc().startOf('day');
export const getTodayUtcDate = () => getTodayUtc().toDate();
