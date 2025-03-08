import { Card, Code, Flex, SimpleGrid, Text } from '@mantine/core';
import { EventStatus, useGetEvents } from '../clients';

import { EventCategoryIcon } from '../components/EventCategoryIcon/EventCategoryIcon';
import { IconExternalLink } from '@tabler/icons-react';
import { Section } from '../components';
import classes from './EventsPage.module.css';
import { useDocumentTitle } from '../helpers';
import { useEventsContext } from '../contexts';

export function EventsPage() {
  useDocumentTitle('Natural Events');
  const { sources } = useEventsContext();
  const [events, eventsResult] = useGetEvents({
    limit: 50,
    status: EventStatus.CLOSED,
    start: new Date('2000-01-04Z'),
    end: new Date('2020-01-04Z'),
  });

  console.log(events);
  console.log(eventsResult);
  const isLoading = eventsResult.isFetching || eventsResult.isPending;

  return (
    <Section isLoading={isLoading}>
      <SimpleGrid className={classes.grid} cols={{ base: 1, xs: 2, md: 3, lg: 4, xl: 5 }} verticalSpacing="lg">
        {events?.events.map((event) => (
          <Card key={event.id} className={classes.card} shadow="sm" padding="lg" radius="md" withBorder>
            <Flex className={classes.categories} gap="xs">
              {event.categories.map((category) => (
                <Code>{category.title}</Code>
              ))}
            </Flex>
            <a href={event.sources[0].eventSourceUrl} target="_blank" title={sources[event.sources[0].id].title}>
              <IconExternalLink className={classes.goToSource} size={14} stroke={1.2} />
            </a>
            <Flex className={classes.header} gap="sm">
              <EventCategoryIcon className={classes.icon} category={event.categories[0]} />
              <Text fw={500} lineClamp={2}>
                {event.title}
              </Text>
            </Flex>
            {event.description ? (
              <Text size="sm" c="dimmed" mt="sm" lineClamp={3}>
                {event.description}
              </Text>
            ) : undefined}
          </Card>
        ))}
      </SimpleGrid>
    </Section>
  );
}
