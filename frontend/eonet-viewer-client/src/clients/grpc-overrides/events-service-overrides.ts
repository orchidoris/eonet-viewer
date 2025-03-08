export * from '../grpc-generated';

import {
  EventStatus,
  EventsContextResponse,
  Event as GrpcEvent,
  EventCategory as GrpcEventCategory,
  EventGeometry as GrpcEventGeometry,
  EventsRequest as GrpcEventsRequest,
  EventsResponse as GrpcEventsResponse,
} from '../grpc-generated';

import { EventCategoryId } from './EventCategoryId';

export interface EventCategory extends Omit<GrpcEventCategory, 'id'> {
  id: EventCategoryId;
}

export interface EventGeometry extends Omit<GrpcEventGeometry, 'date'> {
  date: Date;
}

export interface Event extends Omit<GrpcEvent, 'geometries' | 'closedDate' | 'categories'> {
  categories: EventCategory[];
  closedDate?: Date;
  geometries: EventGeometry[];
}

export interface EventsResponse extends Omit<GrpcEventsResponse, '$typeName' | 'events'> {
  events: Event[];
}

export const defaultEventsResponse: EventsResponse = {
  url: '',
  events: [],
};

export interface EventsRequest
  extends Omit<GrpcEventsRequest, '$typeName' | 'sources' | 'categories' | 'status' | 'start' | 'end'> {
  start?: Date;
  end?: Date;
  sources?: string[];
  categories?: string[];
  status?: EventStatus;
}

export type EventsContext = Omit<EventsContextResponse, '$typeName'>;

export const defaultEventsContext: EventsContext = {
  categories: [],
  layers: [],
  sources: [],
  magnitudes: [],
};
