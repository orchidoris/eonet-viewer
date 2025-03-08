import { Category, EventCategoryId, Layer, Magnitude, Source } from '../../clients';
import { createContext, useContext } from 'react';

export interface EventsContext {
  isLoaded: boolean;
  categories: { [categoryId in EventCategoryId]?: Category };
  layers: { [layerId: string]: Layer };
  sources: { [sourceId: string]: Source };
  magnitudes: { [magnitudeId: string]: Magnitude };
  magnitudesByUnit: { [magnitudeUnit: string]: Magnitude };
}

export const eventsContext = createContext<EventsContext>({
  isLoaded: false,
  categories: {},
  layers: {},
  sources: {},
  magnitudes: {},
  magnitudesByUnit: {},
});

export const useEventsContext = () => useContext(eventsContext);
