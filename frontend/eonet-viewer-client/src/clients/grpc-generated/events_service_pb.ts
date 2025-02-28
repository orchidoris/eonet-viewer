// @generated by protoc-gen-es v2.2.3 with parameter "target=ts"
// @generated from file events_service.proto (package events, syntax proto3)
/* eslint-disable */

import type { GenEnum, GenFile, GenMessage, GenService } from "@bufbuild/protobuf/codegenv1";
import { enumDesc, fileDesc, messageDesc, serviceDesc } from "@bufbuild/protobuf/codegenv1";
import type { Timestamp } from "@bufbuild/protobuf/wkt";
import { file_google_protobuf_timestamp, file_google_protobuf_wrappers } from "@bufbuild/protobuf/wkt";
import type { Message } from "@bufbuild/protobuf";

/**
 * Describes the file events_service.proto.
 */
export const file_events_service: GenFile = /*@__PURE__*/
  fileDesc("ChRldmVudHNfc2VydmljZS5wcm90bxIGZXZlbnRzIoMDCg1FdmVudHNSZXF1ZXN0Eg8KB3NvdXJjZXMYASADKAkSEgoKY2F0ZWdvcmllcxgCIAMoCRIjCgZzdGF0dXMYAyABKA4yEy5ldmVudHMuRXZlbnRTdGF0dXMSEgoFbGltaXQYBCABKAVIAIgBARIRCgRkYXlzGAUgASgFSAGIAQESLgoFc3RhcnQYBiABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wSAKIAQESLAoDZW5kGAcgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcEgDiAEBEi8KCW1hZ25pdHVkZRgIIAEoCzIXLmV2ZW50cy5NYWduaXR1ZGVGaWx0ZXJIBIgBARIuCgxib3VuZGluZ19ib3gYCSABKAsyEy5ldmVudHMuQm91bmRpbmdCb3hIBYgBAUIICgZfbGltaXRCBwoFX2RheXNCCAoGX3N0YXJ0QgYKBF9lbmRCDAoKX21hZ25pdHVkZUIPCg1fYm91bmRpbmdfYm94IlEKD01hZ25pdHVkZUZpbHRlchIKCgJpZBgBIAEoCRIQCgNtaW4YAiABKAFIAIgBARIQCgNtYXgYAyABKAFIAYgBAUIGCgRfbWluQgYKBF9tYXgiZwoLQm91bmRpbmdCb3gSFQoNbWluX2xvbmdpdHVkZRgBIAEoARIUCgxtYXhfbGF0aXR1ZGUYAiABKAESFQoNbWF4X2xvbmdpdHVkZRgDIAEoARIUCgxtaW5fbGF0aXR1ZGUYBCABKAEiLwoORXZlbnRzUmVzcG9uc2USHQoGZXZlbnRzGAEgAygLMg0uZXZlbnRzLkV2ZW50IuQCCgVFdmVudBIKCgJpZBgBIAEoCRINCgV0aXRsZRgCIAEoCRIYCgtkZXNjcmlwdGlvbhgDIAEoCUgAiAEBEgwKBGxpbmsYBCABKAkSNAoLY2xvc2VkX2RhdGUYBSABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wSAGIAQESJwoaY2xvc2VkX2RhdGVfb2Zmc2V0X21pbnV0ZXMYBiABKAVIAogBARIpCgpjYXRlZ29yaWVzGAcgAygLMhUuZXZlbnRzLkV2ZW50Q2F0ZWdvcnkSJAoHc291cmNlcxgIIAMoCzITLmV2ZW50cy5FdmVudFNvdXJjZRIpCgpnZW9tZXRyaWVzGAkgAygLMhUuZXZlbnRzLkV2ZW50R2VvbWV0cnlCDgoMX2Rlc2NyaXB0aW9uQg4KDF9jbG9zZWRfZGF0ZUIdChtfY2xvc2VkX2RhdGVfb2Zmc2V0X21pbnV0ZXMiKgoNRXZlbnRDYXRlZ29yeRIKCgJpZBgBIAEoCRINCgV0aXRsZRgCIAEoCSImCgtFdmVudFNvdXJjZRIKCgJpZBgBIAEoCRILCgN1cmwYAiABKAkijgIKDUV2ZW50R2VvbWV0cnkSKAoEZGF0ZRgBIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASGwoTZGF0ZV9vZmZzZXRfbWludXRlcxgCIAEoBRIMCgR0eXBlGAMgASgJEigKC2Nvb3JkaW5hdGVzGAQgASgLMhMuZXZlbnRzLkNvb3JkaW5hdGVzEhsKDm1hZ25pdHVkZV91bml0GAUgASgJSACIAQESOgoPbWFnbml0dWRlX3ZhbHVlGAYgASgLMhwuZ29vZ2xlLnByb3RvYnVmLkRvdWJsZVZhbHVlSAGIAQFCEQoPX21hZ25pdHVkZV91bml0QhIKEF9tYWduaXR1ZGVfdmFsdWUiMgoLQ29vcmRpbmF0ZXMSEAoIbGF0aXR1ZGUYASABKAESEQoJbG9uZ2l0dWRlGAIgASgBKiwKC0V2ZW50U3RhdHVzEggKBE9QRU4QABIKCgZDTE9TRUQQARIHCgNBTEwQAjJLCg1FdmVudHNTZXJ2aWNlEjoKCUdldEV2ZW50cxIVLmV2ZW50cy5FdmVudHNSZXF1ZXN0GhYuZXZlbnRzLkV2ZW50c1Jlc3BvbnNlQhmqAhZFb25ldFZpZXdlci5BcGkuUHJvdG9zYgZwcm90bzM", [file_google_protobuf_timestamp, file_google_protobuf_wrappers]);

/**
 * Represents filters for querying events from the EONET API.
 *
 * @generated from message events.EventsRequest
 */
export type EventsRequest = Message<"events.EventsRequest"> & {
  /**
   * Filter by event sources, none - means all sources.
   *
   * @generated from field: repeated string sources = 1;
   */
  sources: string[];

  /**
   * Filter by event categories, none - means all categories.
   *
   * @generated from field: repeated string categories = 2;
   */
  categories: string[];

  /**
   * Filter based on events having a closed date.
   *
   * @generated from field: events.EventStatus status = 3;
   */
  status: EventStatus;

  /**
   * Limits the number of events returned.
   *
   * @generated from field: optional int32 limit = 4;
   */
  limit?: number;

  /**
   * Limit the number of prior days (including today) from which events will be returned.
   *
   * @generated from field: optional int32 days = 5;
   */
  days?: number;

  /**
   * Min date of events.
   *
   * @generated from field: optional google.protobuf.Timestamp start = 6;
   */
  start?: Timestamp;

  /**
   * Max date of events.
   *
   * @generated from field: optional google.protobuf.Timestamp end = 7;
   */
  end?: Timestamp;

  /**
   * A ceiling, floor, or range of magnitude values for the events.
   *
   * @generated from field: optional events.MagnitudeFilter magnitude = 8;
   */
  magnitude?: MagnitudeFilter;

  /**
   * A bounding box for event coordinates to fall into.
   *
   * @generated from field: optional events.BoundingBox bounding_box = 9;
   */
  boundingBox?: BoundingBox;
};

/**
 * Describes the message events.EventsRequest.
 * Use `create(EventsRequestSchema)` to create a new message.
 */
export const EventsRequestSchema: GenMessage<EventsRequest> = /*@__PURE__*/
  messageDesc(file_events_service, 0);

/**
 * Ceiling, floor, or range of magnitude values for the events to fall between (inclusive).
 *
 * @generated from message events.MagnitudeFilter
 */
export type MagnitudeFilter = Message<"events.MagnitudeFilter"> & {
  /**
   * Unique id of magnitude unit.
   *
   * @generated from field: string id = 1;
   */
  id: string;

  /**
   * Optional min magnitude value.
   *
   * @generated from field: optional double min = 2;
   */
  min?: number;

  /**
   * Optional min magnitude value.
   *
   * @generated from field: optional double max = 3;
   */
  max?: number;
};

/**
 * Describes the message events.MagnitudeFilter.
 * Use `create(MagnitudeFilterSchema)` to create a new message.
 */
export const MagnitudeFilterSchema: GenMessage<MagnitudeFilter> = /*@__PURE__*/
  messageDesc(file_events_service, 1);

/**
 * Bounding box to filter events by coordinates.
 *
 * @generated from message events.BoundingBox
 */
export type BoundingBox = Message<"events.BoundingBox"> & {
  /**
   * Minimum longitude (left boundary).
   *
   * @generated from field: double min_longitude = 1;
   */
  minLongitude: number;

  /**
   * Maximum latitude (top boundary).
   *
   * @generated from field: double max_latitude = 2;
   */
  maxLatitude: number;

  /**
   * Maximum longitude (right boundary).
   *
   * @generated from field: double max_longitude = 3;
   */
  maxLongitude: number;

  /**
   * Minimum latitude (bottom boundary).
   *
   * @generated from field: double min_latitude = 4;
   */
  minLatitude: number;
};

/**
 * Describes the message events.BoundingBox.
 * Use `create(BoundingBoxSchema)` to create a new message.
 */
export const BoundingBoxSchema: GenMessage<BoundingBox> = /*@__PURE__*/
  messageDesc(file_events_service, 2);

/**
 * Response containing natural events.
 *
 * @generated from message events.EventsResponse
 */
export type EventsResponse = Message<"events.EventsResponse"> & {
  /**
   * A list of natural events.
   *
   * @generated from field: repeated events.Event events = 1;
   */
  events: Event[];
};

/**
 * Describes the message events.EventsResponse.
 * Use `create(EventsResponseSchema)` to create a new message.
 */
export const EventsResponseSchema: GenMessage<EventsResponse> = /*@__PURE__*/
  messageDesc(file_events_service, 3);

/**
 * Natural event.
 *
 * @generated from message events.Event
 */
export type Event = Message<"events.Event"> & {
  /**
   * Unique event id.
   *
   * @generated from field: string id = 1;
   */
  id: string;

  /**
   * Title of the event.
   *
   * @generated from field: string title = 2;
   */
  title: string;

  /**
   * Optional longer description of the event. Most likely only a sentence or two.
   *
   * @generated from field: optional string description = 3;
   */
  description?: string;

  /**
   * Full link to the API endpoint for this specific event.
   *
   * @generated from field: string link = 4;
   */
  link: string;

  /**
   * Event is deemed �closed� when it has ended. The closed field will include a date/time when the event has ended. Depending upon the nature of the event, the closed value may or may not accurately represent the absolute ending of the event. If the event is open, this will show �null�.
   *
   * @generated from field: optional google.protobuf.Timestamp closed_date = 5;
   */
  closedDate?: Timestamp;

  /**
   * Offset for the closed_date field, usually 0 unless the source provided a particular offset.
   *
   * @generated from field: optional int32 closed_date_offset_minutes = 6;
   */
  closedDateOffsetMinutes?: number;

  /**
   * One or more categories assigned to the event.
   *
   * @generated from field: repeated events.EventCategory categories = 7;
   */
  categories: EventCategory[];

  /**
   * One or more sources that refer to more information about the event.
   *
   * @generated from field: repeated events.EventSource sources = 8;
   */
  sources: EventSource[];

  /**
   * One or more event geometries are the pairing of a specific date/time with a location. The date/time will most likely be 00:00Z unless the source provided a particular time. The geometry will be a GeoJSON object of either type �Point� or �Polygon�.
   *
   * @generated from field: repeated events.EventGeometry geometries = 9;
   */
  geometries: EventGeometry[];
};

/**
 * Describes the message events.Event.
 * Use `create(EventSchema)` to create a new message.
 */
export const EventSchema: GenMessage<Event> = /*@__PURE__*/
  messageDesc(file_events_service, 4);

/**
 * Mapping from a natural event to a category.
 *
 * @generated from message events.EventCategory
 */
export type EventCategory = Message<"events.EventCategory"> & {
  /**
   * Unique category id.
   *
   * @generated from field: string id = 1;
   */
  id: string;

  /**
   * Title of the category.
   *
   * @generated from field: string title = 2;
   */
  title: string;
};

/**
 * Describes the message events.EventCategory.
 * Use `create(EventCategorySchema)` to create a new message.
 */
export const EventCategorySchema: GenMessage<EventCategory> = /*@__PURE__*/
  messageDesc(file_events_service, 5);

/**
 * Mapping from a natural event to a source.
 *
 * @generated from message events.EventSource
 */
export type EventSource = Message<"events.EventSource"> & {
  /**
   * Unique source id.
   *
   * @generated from field: string id = 1;
   */
  id: string;

  /**
   * Url of this specific event in the original source.
   *
   * @generated from field: string url = 2;
   */
  url: string;
};

/**
 * Describes the message events.EventSource.
 * Use `create(EventSourceSchema)` to create a new message.
 */
export const EventSourceSchema: GenMessage<EventSource> = /*@__PURE__*/
  messageDesc(file_events_service, 6);

/**
 * Geometry of a natural event.
 *
 * @generated from message events.EventGeometry
 */
export type EventGeometry = Message<"events.EventGeometry"> & {
  /**
   * Date and time of the event.
   *
   * @generated from field: google.protobuf.Timestamp date = 1;
   */
  date?: Timestamp;

  /**
   * Offset for the date field, usually 0 unless the source provided a particular offset.
   *
   * @generated from field: int32 date_offset_minutes = 2;
   */
  dateOffsetMinutes: number;

  /**
   * Type of geometry, usually "Point" but "Polygon" is possible.
   *
   * @generated from field: string type = 3;
   */
  type: string;

  /**
   * Coordinates of the event.
   *
   * @generated from field: events.Coordinates coordinates = 4;
   */
  coordinates?: Coordinates;

  /**
   * Unit of the magnitude value.
   *
   * @generated from field: optional string magnitude_unit = 5;
   */
  magnitudeUnit?: string;

  /**
   * Magnitude value of the event.
   *
   * @generated from field: optional google.protobuf.DoubleValue magnitude_value = 6;
   */
  magnitudeValue?: number;
};

/**
 * Describes the message events.EventGeometry.
 * Use `create(EventGeometrySchema)` to create a new message.
 */
export const EventGeometrySchema: GenMessage<EventGeometry> = /*@__PURE__*/
  messageDesc(file_events_service, 7);

/**
 * Position of event geometry
 *
 * @generated from message events.Coordinates
 */
export type Coordinates = Message<"events.Coordinates"> & {
  /**
   * Latitude of the position.
   *
   * @generated from field: double latitude = 1;
   */
  latitude: number;

  /**
   * Longitude of the position.
   *
   * @generated from field: double longitude = 2;
   */
  longitude: number;
};

/**
 * Describes the message events.Coordinates.
 * Use `create(CoordinatesSchema)` to create a new message.
 */
export const CoordinatesSchema: GenMessage<Coordinates> = /*@__PURE__*/
  messageDesc(file_events_service, 8);

/**
 * Filter based on events having a closed date.
 *
 * @generated from enum events.EventStatus
 */
export enum EventStatus {
  /**
   * Events have no closed date.
   *
   * @generated from enum value: OPEN = 0;
   */
  OPEN = 0,

  /**
   * Events have a closed date.
   *
   * @generated from enum value: CLOSED = 1;
   */
  CLOSED = 1,

  /**
   * All events, regardless of having a closed date or not.
   *
   * @generated from enum value: ALL = 2;
   */
  ALL = 2,
}

/**
 * Describes the enum events.EventStatus.
 */
export const EventStatusSchema: GenEnum<EventStatus> = /*@__PURE__*/
  enumDesc(file_events_service, 0);

/**
 * The events service definition.
 *
 * @generated from service events.EventsService
 */
export const EventsService: GenService<{
  /**
   * Gets events based on the provided filters.
   *
   * @generated from rpc events.EventsService.GetEvents
   */
  getEvents: {
    methodKind: "unary";
    input: typeof EventsRequestSchema;
    output: typeof EventsResponseSchema;
  },
}> = /*@__PURE__*/
  serviceDesc(file_events_service, 0);

