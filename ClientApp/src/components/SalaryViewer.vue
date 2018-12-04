<template>
  <div>
    {{ salary.From }}
    <template v-if="hasSalaryInfo">
      -
    </template>
    {{ salary.To }}
    {{ salary.Currency }}
    <template v-if="hasSalaryInfo">
      {{ salary.Gross ? 'до вычета НДФЛ' : 'на руки' }}
    </template>
  </div>
</template>

<script>
  export default {
    name: 'SalaryViewer',
    props: {
      salary: {
        type: Object,
        required: true,
      },
    },
    computed: {
      hasSalaryInfo () {
        return this.salary.From || this.salary.To
      },
    },
  }
</script>

<style scoped>

</style>
