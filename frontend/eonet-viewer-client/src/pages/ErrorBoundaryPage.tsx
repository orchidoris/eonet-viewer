import { useDocumentTitle } from '../helpers';

export function ErrorBoundaryPage() {
  useDocumentTitle('Oops!');

  return <div>Oops! Something went wrong!</div>;
}
