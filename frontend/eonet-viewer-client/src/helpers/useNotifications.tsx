import { DefaultMantineColor, MantineTheme, useMantineTheme } from '@mantine/core';

import { IconX } from '@tabler/icons-react';
import { notifications as mantineNotifications } from '@mantine/notifications';

export type NotificationIcon = 'X';
interface NotificationIconProperties {
  color: string;
  icon: React.ReactNode;
}

const getNotificationIcon = (theme: MantineTheme, icon?: NotificationIcon): NotificationIconProperties | undefined => {
  if (!icon) return undefined;

  switch (icon) {
    case 'X':
      return { color: theme.colors.red[6], icon: <IconX /> };

    default:
      throw new Error(`Unhandled notification icon type: ${icon}`);
  }
};

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
    showInfo: (notification: { message: string; title?: string }) =>
      showNotification({ ...notification, color: theme.colors.green[7] }),
    showError: ({ title = 'Error', ...notification }: { message: string; title?: string }) =>
      showNotification({ title, ...notification, ...getNotificationIcon(theme, 'X') }),
  };
};
