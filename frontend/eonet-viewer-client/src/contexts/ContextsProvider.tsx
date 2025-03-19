import { EventsContextProvider } from './EventsContext';
import { ReactNode } from 'react';

interface ContextsProviderProps {
  children?: ReactNode;
}

export function ContextsProvider({ children }: ContextsProviderProps) {
  return <EventsContextProvider>{children}</EventsContextProvider>;
}
