import { EventStatus, EventsRequest } from '../../clients';
import { EventsFiltersFormStatus, EventsFiltersFormValues } from '../../components';

import { useMemo } from 'react';

const getRequestStatus = (status?: EventsFiltersFormStatus): EventStatus => {
  if (!status) return EventStatus.OPEN;

  switch (status) {
    case 'open':
      return EventStatus.OPEN;
    case 'closed':
      return EventStatus.CLOSED;
    case 'all':
    default:
      return EventStatus.ALL;
  }
};

export const useEventsRequest = (filters: Partial<EventsFiltersFormValues>): EventsRequest =>
  useMemo(
    () => ({
      categories: filters.categories ?? [],
      sources: filters.sources ?? [],
      status: getRequestStatus(filters.status),
      start: filters.dates?.[0] ?? undefined,
      end: filters.dates?.[1] ?? undefined,
    }),
    [filters],
  );
