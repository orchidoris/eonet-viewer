import { Category, EventCategoryId, Layer, Magnitude, Source } from '../../clients';
import { createContext, useContext } from 'react';

export type EventsContextCategories = { [categoryId in EventCategoryId]?: Category };
export type EventsContextSources = { [sourceId: string]: Source };

export interface EventsContext {
  isLoading: boolean;
  categories: EventsContextCategories;
  layers: { [layerId: string]: Layer };
  sources: EventsContextSources;
  magnitudes: { [magnitudeId: string]: Magnitude };
  magnitudesByUnit: { [magnitudeUnit: string]: Magnitude };
}

export const eventsContext = createContext<EventsContext>({
  isLoading: false,
  categories: {},
  layers: {},
  sources: {},
  magnitudes: {},
  magnitudesByUnit: {},
});

export const useEventsContext = () => useContext(eventsContext);
