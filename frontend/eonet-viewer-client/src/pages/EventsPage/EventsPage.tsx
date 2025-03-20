import { EventsFilters, EventsFiltersFormValues, Page } from '../../components';
import { getTodayUtc, useDocumentTitle } from '../../helpers';
import { useEventsContextState, useUIState } from '../../state';

import { EventsGrid } from '../../components/EventsGrid';
import classes from './EventsPage.module.css';
import { useEventsRequest } from './EventsPage.core';
import { useGetEvents } from '../../clients';
import { useState } from 'react';

export function EventsPage() {
  useDocumentTitle('Natural Events');

  const { filtersVisible } = useUIState();
  const { isLoading: contextIsLoading } = useEventsContextState();

  const [filters, setFilters] = useState<Partial<EventsFiltersFormValues>>(() => ({
    dates: [getTodayUtc().subtract(10, 'day').toDate(), getTodayUtc().toDate()],
  }));

  const eventsRequest = useEventsRequest(filters);
  const [events, eventsResult] = useGetEvents(eventsRequest);

  const isLoading = eventsResult.isFetching || eventsResult.isPending || contextIsLoading;

  return (
    <Page className={classes.root} isLoading={isLoading} {...(filtersVisible && { 'data-filters-visible': '' })}>
      <div className={classes.filtersContainer}>
        <EventsFilters className={classes.filters} value={filters} onSubmit={setFilters} />
      </div>
      <EventsGrid events={events.events} className={classes.events} />
    </Page>
  );
}
