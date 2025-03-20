import {
  InitialDataFunction,
  QueryKey,
  UseMutationOptions,
  UseMutationResult,
  UseQueryOptions,
  UseQueryResult,
  useMutation as tanstackUseMutation,
  useQuery as tanstackUseQuery,
} from '@tanstack/react-query';
import { env, useNotifications } from '../helpers';

import { ConnectError } from '@connectrpc/connect';
import { createGrpcWebTransport } from '@connectrpc/connect-web';
import { useEffect } from 'react';

export const grpcWebTransport = createGrpcWebTransport({
  baseUrl: env.apiBaseUrl,
});

// eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/no-unsafe-member-access
const isConnectError = (error: any): error is ConnectError => error?.name === 'ConnectError';

export const MAX_ERROR_MESSAGE_LEN = 250;
export const cropMessage = (message: string) =>
  message.length > MAX_ERROR_MESSAGE_LEN ? `${message.substring(0, MAX_ERROR_MESSAGE_LEN)}...` : message;

export const getErrorNotification = (error: unknown): { title: string; message: string } => {
  if (isConnectError(error)) return { title: `Network error`, message: cropMessage(error.rawMessage) };

  return { title: `Unknown error`, message: JSON.stringify(error) };
};

interface RequestNotificationsParams {
  successNotification?: {
    message: string;
    title?: string;
  };
}

const useShowNotifications = (
  error: unknown,
  isSuccess: boolean,
  { successNotification }: RequestNotificationsParams,
) => {
  const notifications = useNotifications();

  useEffect(() => {
    if (error) notifications.showError(getErrorNotification(error));
  }, [error, notifications]);

  useEffect(() => {
    if (successNotification && isSuccess)
      notifications.show({ title: successNotification.title ?? 'Success', ...successNotification, color: 'green' });
  }, [successNotification, isSuccess, notifications]);
};

export const useQuery = <
  TQueryFnData = unknown,
  TInitialData extends TQueryFnData | InitialDataFunction<TQueryFnData> | undefined =
    | TQueryFnData
    | InitialDataFunction<TQueryFnData>
    | undefined,
  TError = unknown,
  TData = TQueryFnData,
  TQueryKey extends QueryKey = QueryKey,
>({
  successNotification,
  ...options
}: Omit<UseQueryOptions<TQueryFnData, TError, TData, TQueryKey>, 'initialData'> & {
  initialData?: TInitialData;
} & RequestNotificationsParams): [
  TInitialData extends undefined ? TData | undefined : TData,
  Omit<UseQueryResult<TData, TError>, 'data'>,
] => {
  const { data, ...result } = tanstackUseQuery(options);
  useShowNotifications(result.error, result.isSuccess, { successNotification });

  const typedData: TData | undefined = data;
  return [typedData as TInitialData extends undefined ? TData | undefined : TData, result];
};

export const useMutation = <TData = unknown, TError = unknown, TVariables = void, TContext = unknown>({
  successNotification,
  ...options
}: UseMutationOptions<TData, TError, TVariables, TContext> & RequestNotificationsParams): UseMutationResult<
  TData,
  TError,
  TVariables,
  TContext
> => {
  const result = tanstackUseMutation(options);
  useShowNotifications(result.error, result.isSuccess, { successNotification });

  return result;
};
