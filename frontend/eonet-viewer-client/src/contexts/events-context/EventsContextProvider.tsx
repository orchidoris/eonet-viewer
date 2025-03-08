import { EventsContext, eventsContext } from './useEventsContext';
import { ReactNode, useMemo } from 'react';

import { useGetEventsContext } from '../../clients';

interface EventsContextProviderProps {
  children?: ReactNode;
}

export function EventsContextProvider({ children }: EventsContextProviderProps) {
  const [{ categories, layers, sources, magnitudes }, eventsContextResult] = useGetEventsContext();
  const value = useMemo(
    (): EventsContext => ({
      isLoaded: eventsContextResult.isSuccess,
      categories: categories.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      layers: layers.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      sources: sources.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      magnitudes: magnitudes.reduce((acc, item) => ({ ...acc, [item.id]: item }), {}),
      magnitudesByUnit: magnitudes.reduce((acc, item) => ({ ...acc, [item.unit]: item }), {}),
    }),
    [eventsContextResult.isSuccess, categories, layers, sources, magnitudes],
  );

  return <eventsContext.Provider value={value}>{children}</eventsContext.Provider>;
}
