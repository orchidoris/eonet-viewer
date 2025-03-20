import { DefaultMantineColor, MantineTheme, useMantineTheme } from '@mantine/core';

import { IconX } from '@tabler/icons-react';
import { notifications as mantineNotifications } from '@mantine/notifications';
import { useCallback } from 'react';

export type NotificationIcon = 'X' | '';
interface NotificationIconProperties {
  color: string;
  icon: React.ReactNode;
}

const getErrorNotificationIcon = (theme: MantineTheme): NotificationIconProperties | undefined => ({
  color: theme.colors.red[6],
  icon: <IconX />,
});

function showNotification(notification: {
  title?: string;
  message: string;
  color?: DefaultMantineColor;
  icon?: React.ReactNode;
}) {
  mantineNotifications.show({
    ...notification,
    autoClose: 100000,
  });
}

export const useNotifications = () => {
  const theme = useMantineTheme();
  return {
    show: showNotification,
    showInfo: useCallback(
      (notification: { message: string; title?: string }) => {
        showNotification({ ...notification, color: theme.colors.green[7] });
      },
      [theme.colors.green],
    ),
    showError: useCallback(
      ({ title = 'Error', ...notification }: { message: string; title?: string }) => {
        showNotification({ title, ...notification, ...getErrorNotificationIcon(theme) });
      },
      [theme],
    ),
  };
};
