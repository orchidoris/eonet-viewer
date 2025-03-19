import { EventsContext, eventsContext } from './useEventsContext';
import { ReactNode, useMemo } from 'react';

import { useGetEventsContext } from '../../clients';

interface EventsContextProviderProps {
  children?: ReactNode;
}

export function EventsContextProvider({ children }: EventsContextProviderProps) {
  const [{ categories, layers, sources, magnitudes }, result] = useGetEventsContext();
  const value = useMemo(
    (): EventsContext => ({
      isLoading: result.isFetching || result.isPending,
      categories: categories.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      layers: layers.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      sources: sources.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      magnitudes: magnitudes.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      magnitudesByUnit: magnitudes.reduce((acc, item) => ({ ...acc, [item.unit]: item }), {}),
    }),
    [categories, layers, sources, magnitudes, result],
  );

  return <eventsContext.Provider value={value}>{children}</eventsContext.Provider>;
}
