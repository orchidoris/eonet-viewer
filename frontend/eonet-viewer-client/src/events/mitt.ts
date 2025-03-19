import mitt from 'mitt';
import { useEffect } from 'react';

type Events = {
  'filters-toggle': void;
};

export const emitter = mitt<Events>();

export const emitFiltersToggle = () => emitter.emit('filters-toggle');
export const useOnFiltersToggle = (onFiltersToggle: () => void) =>
  useEffect(() => {
    emitter.on('filters-toggle', onFiltersToggle);
    return () => emitter.off('filters-toggle');
  }, [onFiltersToggle]);
