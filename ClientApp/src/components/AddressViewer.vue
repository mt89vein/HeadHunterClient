<template>
  <div :class="{'small-text': small}">
    {{ address.City }}
    {{ address.Street }}
    {{ address.Building }}
    {{ address.Description }}
  </div>
</template>

<script>
  export default {
    name: 'AddressViewer',
    props: {
      address: {
        type: Object,
        required: true,
      },
      small: {
        type: Boolean,
        required: false,
        default: () => true,
      },
    },
  }
</script>

<style scoped>
  .small-text {
    font-size: 13px;
  }
</style>
