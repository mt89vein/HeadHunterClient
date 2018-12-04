<template>
  <FetchData
    url="/api/vacancy"
    :filter="filter"
  >
    <template slot-scope="{ json }">
      <span v-if="!json || !json.Count">
        Загрузка...
      </span>
      <div v-else class="grid-container">
        <ElCard
          v-for="(vacancy, index) in json.Objects"
          :key="index"
          shadow="hover"
        >
          <div slot="header">
            <strong class="vacancy-title">
              <a :href="`https://hh.ru/vacancy/${vacancy.ExternalId}`" target="_blank">
                {{ vacancy.Name }}
              </a>
            </strong>
            <div>
              <a :href="`https://hh.ru/employer/${vacancy.Employer.ExternalId}`" target="_blank">
                {{ vacancy.Employer.Name }}
              </a>
            </div>
            <SalaryViewer :salary="vacancy.Salary"/>
            <AddressViewer small :address="vacancy.Address"/>
          </div>
          <div>
            {{ vacancy.Description }}
          </div>
        </ElCard>
      </div>
    </template>
  </FetchData>
</template>

<script>
  import { Card } from 'element-ui'
  import FetchData from '../shared/components/FetchData'
  import AddressViewer from './AddressViewer'
  import SalaryViewer from './SalaryViewer'

  export default {
    name: 'VacancyList',
    components: {
      AddressViewer,
      SalaryViewer,
      FetchData,
      ElCard: Card,
    },
    props: {
      filter: {
        type: Object,
        required: true,
      },
    },
  }
</script>

<style scoped lang="scss">
  a {
    color: #42b983;
  }

  .grid-container {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    grid-column-gap: 10px;
  }
</style>
