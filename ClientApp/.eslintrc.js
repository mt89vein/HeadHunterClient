module.exports = {
  root: true,
  env: {
    node: true,
    es6: true,
  },
  'extends': [
    'plugin:vue/recommended',
    '@vue/standard',
  ],
  plugins: [
    'vue',
    'html',
  ],
  rules: {
    'no-console': process.env.NODE_ENV === 'production'
                  ? 'error'
                  : 'off',
    'no-debugger': process.env.NODE_ENV === 'production'
                   ? 'error'
                   : 'off',
    'vue/html-indent': [
      'error', 2, {
        'alignAttributesVertically': true,
      },
    ],
    'no-unneeded-ternary': [
      'error',
      {
        defaultAssignment: false,
      },
    ],
    'arrow-parens': 0,
    'generator-star-spacing': 'off',
    'no-console': 'off',
    'no-debugger': 'off',
    'no-tabs': 0,
    indent: [
      'error',
      2,
    ],
    quotes: [
      'error',
      'single',
    ],
    semi: [
      'error',
      'never',
    ],
    'indent-size': [
      true,
      2,
    ],
    'comma-dangle': [
      'error',
      'always-multiline',
    ],
    'vue/attribute-hyphenation': [
      'error',
      'always',
    ],
    'vue/html-end-tags': 'error',
    'vue/html-self-closing': 'error',
    'vue/require-prop-types': 'error',
    'vue/attributes-order': 'error',
    'vue/html-quotes': [
      'error',
      'double',
    ],
    'indent': 'off',
    'vue/script-indent': [
      'error', 2, {
        'baseIndent': 1,
        'switchCase': 1,
        'ignores': [],
      },
    ],
    'vue/order-in-components': 'error',
  },
  'overrides': [
    {
      'files': ['*.vue'],
      'rules': {
        'indent': 'off',
      },
    },
  ],
  parserOptions: {
    parser: 'babel-eslint',
  },
}
