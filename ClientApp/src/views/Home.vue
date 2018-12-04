<template>
  <div>
    <FilterForm :filter.sync="filter" />
    <VacancyList :filter="filter" />
  </div>
</template>

<script>
  import FilterForm from '../components/VacancyFilterForm'
  import VacancyList from '../components/VacancyList'

  export default {
    name: 'Home',
    components: {
      FilterForm,
      VacancyList,
    },
    data () {
      return {
        filter: {
          Text: null,
          DateFrom: null,
          DateTo: null,
          SearchFields: [],
          EmploymentTypes: [],
          ScheduleTypes: [],
          SpecializationExternalIds: [],
          Experience: null,
          SalaryFilter: {
            Salary: null,
            OnlyWithSalary: false,
            CurrencyCode: 'RUR',
          },
        },
      }
    },
  }
</script>
