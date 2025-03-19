import { EventsFilters, EventsFiltersFormValues, Page } from '../components';
import { getTodayUtc, useDocumentTitle } from '../helpers';
import { useMemo, useState } from 'react';

import { EventsGrid } from '../components/EventsGrid';
import classes from './EventsPage.module.css';
import { getEventsRequest } from './EventsPage.core';
import { useEventsContext } from '../contexts';
import { useGetEvents } from '../clients';
import { useOnFiltersToggle } from '../events';
import { useToggle } from '@mantine/hooks';

export function EventsPage() {
  useDocumentTitle('Natural Events');
  const { isLoading: contextIsLoading } = useEventsContext();
  const [filtersVisible, toggleFilters] = useToggle([true, false]);
  useOnFiltersToggle(() => toggleFilters());

  const [filters, setFilters] = useState<Partial<EventsFiltersFormValues>>(() => ({
    dates: [getTodayUtc().subtract(10, 'day').toDate(), getTodayUtc().toDate()],
  }));

  const eventsRequest = useMemo(() => getEventsRequest(filters), [filters]);
  const [events, eventsResult] = useGetEvents(eventsRequest);

  const isLoading = eventsResult.isFetching || eventsResult.isPending || contextIsLoading;

  return (
    <Page className={classes.root} isLoading={isLoading} {...(filtersVisible && { 'data-filters-visible': '' })}>
      <div className={classes.filtersContainer}>
        <EventsFilters className={classes.filters} value={filters} onSubmit={setFilters} />
      </div>
      <EventsGrid events={events?.events} className={classes.events} />
    </Page>
  );
}
