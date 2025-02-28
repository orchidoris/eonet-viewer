import { EventsService } from '../clients/grpc-generated/events_service_pb';
import { createClient } from '@connectrpc/connect';
import { createConnectTransport } from '@connectrpc/connect-web';
import { env } from '../helpers/env';
import { useDocumentTitle } from '../helpers';
import { useEffect } from 'react';

// Import service definition that you want to connect to.



// The transport defines what type of endpoint we're hitting.
// In our example we'll be communicating with a Connect endpoint.
const transport = createConnectTransport({
  baseUrl: env.apiBaseUrl,
});

// Here we make the client itself, combining the service
// definition with the transport.
const client = createClient(EventsService, transport);

export function EventsPage() {
  useDocumentTitle('Natural Events');

  return (
    <form
      onSubmit={async (e) => {
        e.preventDefault();
        await EventsService.method({
          sentence: inputValue,
        });
      }}
    >
      <input value={inputValue} onChange={(e) => setInputValue(e.target.value)} />
      <button type="submit">Send</button>
    </form>
  );
}
