export * from '../grpc-generated';

import {
  EventStatus,
  EventsContextResponse,
  Category as GrpcCategory,
  Event as GrpcEvent,
  EventCategory as GrpcEventCategory,
  EventGeometry as GrpcEventGeometry,
  EventsRequest as GrpcEventsRequest,
  EventsResponse as GrpcEventsResponse,
} from '../grpc-generated';

import { EventCategoryId } from './EventCategoryId';

export interface EventCategory extends Omit<GrpcEventCategory, 'id' | '$typeName'> {
  id: EventCategoryId;
}

export const defaultEventCategory: EventCategory = {
  id: EventCategoryId.None,
  title: '',
};

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

export interface Category extends Omit<GrpcCategory, 'id' | '$typeName'> {
  id: EventCategoryId;
}

export interface EventsContext extends Omit<EventsContextResponse, 'categories' | '$typeName'> {
  categories: Category[];
}

export const defaultEventsContext: EventsContext = {
  categories: [],
  layers: [],
  sources: [],
  magnitudes: [],
};
