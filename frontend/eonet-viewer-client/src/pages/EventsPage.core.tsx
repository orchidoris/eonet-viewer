import { EventStatus, EventsRequest } from '../clients';
import { EventsFiltersFormStatus, EventsFiltersFormValues } from '../components';

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

export const getEventsRequest = (values: Partial<EventsFiltersFormValues>): EventsRequest => ({
  categories: values.categories ?? [],
  sources: values.sources ?? [],
  status: getRequestStatus(values.status),
  start: values.dates?.[0] ?? undefined,
  end: values.dates?.[1] ?? undefined,
});
