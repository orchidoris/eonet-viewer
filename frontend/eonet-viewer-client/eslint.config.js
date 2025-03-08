import cssModules from 'eslint-plugin-css-modules';
import globals from 'globals';
import js from '@eslint/js';
import prettier from 'eslint-config-prettier';
import prettierPlugin from 'eslint-plugin-prettier';
import reactHooks from 'eslint-plugin-react-hooks';
import reactRefresh from 'eslint-plugin-react-refresh';
import tanstackQuery from '@tanstack/eslint-plugin-query';
import tseslint from 'typescript-eslint';

export default tseslint.config(
  { ignores: ['dist', '**/*_pb.ts', '*.css'] },
  {
    extends: [js.configs.recommended, ...tseslint.configs.recommended, prettier],
    files: ['**/*.{ts,tsx}'],
    languageOptions: {
      ecmaVersion: 2020,
      globals: globals.browser,
    },
    plugins: {
      'react-hooks': reactHooks,
      'react-refresh': reactRefresh,
      prettier: prettierPlugin,
      '@tanstack/query': tanstackQuery,
      'css-modules': cssModules,
    },
    rules: {
      ...reactHooks.configs.recommended.rules,
      'react-refresh/only-export-components': ['warn', { allowConstantExport: true }],
      'import/prefer-default-export': 'off',
      'prettier/prettier': 'error',
      'css-modules/no-unused-class': 'warn',
      'css-modules/no-undef-class': 'error',
      'react-hooks/exhaustive-deps': 'error',
    },
  },
);
