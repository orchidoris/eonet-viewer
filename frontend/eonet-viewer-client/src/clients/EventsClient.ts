import {
  EventCategoryId,
  EventStatus,
  EventsContext,
  EventsRequest,
  EventsResponse,
  EventsService,
  defaultEventsContext,
  defaultEventsResponse,
} from './grpc-overrides';
import { grpcWebTransport, useQuery } from './core';
import { timestampDate, timestampFromDate } from '@bufbuild/protobuf/wkt';

import { createClient } from '@connectrpc/connect';

const client = createClient(EventsService, grpcWebTransport);

const getEvents = async (request: EventsRequest): Promise<EventsResponse> => {
  const response = await client.getEvents({
    ...request,
    start: request.start ? timestampFromDate(request.start) : undefined,
    end: request.end ? timestampFromDate(request.end) : undefined,
    sources: request.sources ?? [],
    categories: request.categories ?? [],
    status: request.status ?? EventStatus.OPEN,
  });

  return {
    ...response,
    events: response.events.map((event) => ({
      ...event,
      closedDate: event.closedDate ? timestampDate(event.closedDate) : undefined,
      categories: event.categories.map((category) => ({
        ...category,
        id: category.id as EventCategoryId,
      })),
      sources: event.sources,
      geometries: event.geometries.map((geometry) => ({
        ...geometry,
        date: geometry.date ? timestampDate(geometry.date) : new Date(),
      })),
    })),
  };
};

export const useGetEvents = (request: EventsRequest) =>
  useQuery({
    queryKey: ['getEvents', JSON.stringify(request)],
    queryFn: () => getEvents(request),
    initialData: defaultEventsResponse,
  });

const getEventsContext = async (): Promise<EventsContext> => {
  const response = await client.getEventsContext({});
  return {
    ...response,
    categories: response.categories.map((category) => ({
      ...category,
      id: category.id as EventCategoryId,
    })),
  };
};

export const useGetEventsContext = () =>
  useQuery({
    queryKey: ['getEventsContext'],
    queryFn: () => getEventsContext(),
    initialData: defaultEventsContext,
  });
