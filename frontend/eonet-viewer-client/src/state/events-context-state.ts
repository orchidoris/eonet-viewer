import { Category, EventCategoryId, Layer, Magnitude, Source, useGetEventsContext } from '../clients';

import { create } from 'zustand';
import { useEffect } from 'react';

interface EventsContextMap<Value = unknown, Key extends string = string> {
  keys: Key[];
  values: Value[];
  get: (key: Key) => Value | undefined;
}

const getEventsContextMap = <Value, Key extends string>(
  values: Value[],
  getKey: (value: Value) => Key,
): EventsContextMap<Value, Key> => {
  const map = values.reduce((acc, value) => ({ ...acc, [getKey(value)]: value }), {} as Record<Key, Value>);
  return { keys: values.map(getKey), values, get: (key: Key) => map[key] };
};

interface EventsContextStateData {
  isLoading: boolean;
  categories: EventsContextMap<Category, EventCategoryId>;
  sources: EventsContextMap<Source>;
  layers: EventsContextMap<Layer>;
  magnitudes: EventsContextMap<Magnitude>;
  magnitudesByUnit: EventsContextMap<Magnitude>;
}

interface EventsContextStateActions {
  set: (data: EventsContextStateData) => void;
}

const getDefaultMap = <Value, Key extends string = string>(): EventsContextMap<Value, Key> => ({
  keys: [],
  values: [],
  get: () => undefined,
});

const useFullEventsContextState = create<EventsContextStateData & EventsContextStateActions>((set) => ({
  isLoading: true,
  categories: getDefaultMap<Category, EventCategoryId>(),
  sources: getDefaultMap<Source>(),
  layers: getDefaultMap<Layer>(),
  magnitudes: getDefaultMap<Magnitude>(),
  magnitudesByUnit: getDefaultMap<Magnitude>(),
  set,
}));

export const useEventsContextState = (): EventsContextStateData => {
  const state = useFullEventsContextState();
  return {
    isLoading: state.isLoading,
    categories: state.categories,
    sources: state.sources,
    layers: state.layers,
    magnitudes: state.magnitudes,
    magnitudesByUnit: state.magnitudesByUnit,
  };
};

export const useLoadEventsContextState = () => {
  const { set } = useFullEventsContextState();
  const [eventsContext, eventContextResult] = useGetEventsContext();
  const { isLoading, isSuccess } = eventContextResult;
  useEffect(() => {
    if (!isLoading && isSuccess) {
      const { categories, sources, layers, magnitudes } = eventsContext;
      set({
        isLoading: false,
        categories: getEventsContextMap(categories, (category) => category.id),
        sources: getEventsContextMap(sources, (source) => source.id),
        layers: getEventsContextMap(layers, (layer) => layer.id),
        magnitudes: getEventsContextMap(magnitudes, (magnitude) => magnitude.id),
        magnitudesByUnit: getEventsContextMap(magnitudes, (magnitude) => magnitude.unit),
      });
    }
  }, [eventsContext, isLoading, isSuccess, set]);
};
