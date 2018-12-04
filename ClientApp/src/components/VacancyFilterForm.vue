<template>
  <ElForm :model="filter" label-width="200px">
    <ElFormItem label="Текст для поиска">
      <ElCol :span="12">
        <ElInput v-model="filter.Text"/>
      </ElCol>
      <ElCol :span="12">
        <ElSelect
          v-model="filter.SearchFields"
          multiple
          collapse-tags
          placeholder="Искать в"
        >
          <ElOption
            v-for="item in selectFields"
            :key="item.Value"
            :label="item.Text"
            :value="item.Value"/>
        </ElSelect>
      </ElCol>
    </ElFormItem>
    <ElFormItem label="Дата публикации">
      <ElDatePicker
        v-model="dates"
        type="daterange"
        range-separator="по"
        placeholder="Выберите даты"
        format="dd.MM.yyyy"
        value-format="yyyy-MM-dd"
        style="width: 100%;"
        @change="onDateChange"
      />
    </ElFormItem>
    <ElFormItem label="Зарплата">
      <ElCol :span="15">
        <ElInput v-model.number="filter.SalaryFilter.Salary">
          <ElSelect
            slot="prepend"
            v-model="filter.SalaryFilter.CurrencyCode"
            filterable
            remote
            searchhable
            reserve-keyword
            :remote-method="searchCurrency"
            :loading="loadingCurrency"
          >
            <ElOption
              v-for="item in currencies"
              :key="item.Code"
              :label="item.Name"
              :value="item.Code"/>
          </ElSelect>
        </ElInput>
      </ElCol>
      <ElCol :span="9">
        <ElSwitch
          v-model="filter.SalaryFilter.OnlyWithSalary"
          active-text="Скрыть вакансии без указания зарплаты"
        />
      </ElCol>
    </ElFormItem>
    <ElFormItem label="Опыт работы">
      <ElSelect v-model="filter.Experience" clearable>
        <ElOption
          v-for="item in experienceTypes"
          :key="item.Value"
          :label="item.Text"
          :value="item.Value"/>
      </ElSelect>
    </ElFormItem>
    <ElFormItem label="Тип занятости">
      <ElSelect v-model="filter.EmploymentTypes" multiple>
        <ElOption
          v-for="item in employmentTypes"
          :key="item.Value"
          :label="item.Text"
          :value="item.Value"/>
      </ElSelect>
    </ElFormItem>
    <ElFormItem label="График работы">
      <ElSelect v-model="filter.ScheduleTypes" multiple>
        <ElOption
          v-for="item in scheduleTypes"
          :key="item.Value"
          :label="item.Text"
          :value="item.Value"/>
      </ElSelect>
    </ElFormItem>
    <ElFormItem label="Специализации">
      <ElSelect
        v-model="filter.SpecializationExternalIds"
        multiple
        filterable
        remote
        searchhable
        reserve-keyword
        :remote-method="searchSpecializations"
        :loading="loadingSpecializations"
      >
        <ElOption
          v-for="item in specializations"
          :key="item.ExternalId"
          :label="item.Name"
          :value="item.ExternalId"
        />
      </ElSelect>
    </ElFormItem>
    <ElFormItem>
      <ElButton type="primary" @click="onSubmit">Поиск!</ElButton>
    </ElFormItem>
  </ElForm>
</template>

<script>
  import { Button, Col, DatePicker, Form, FormItem, Input, Option, Select, Switch } from 'element-ui'
  import { HTTP } from '../shared/http'

  export default {
    name: 'FilterForm',
    components: {
      ElForm: Form,
      ElFormItem: FormItem,
      ElInput: Input,
      ElCol: Col,
      ElDatePicker: DatePicker,
      ElSwitch: Switch,
      ElSelect: Select,
      ElOption: Option,
      ElButton: Button,
    },
    data () {
      const selectFields = [
        {
          Value: 'Name', Text: 'В названии вакансии',
        },
        {
          Value: 'CompanyName', Text: 'В названии компании',
        },
        {
          Value: 'Description', Text: 'В описании вакансии',
        },
      ]
      return {
        filter: {
          Text: null,
          DateFrom: null,
          DateTo: null,
          SearchFields: [...selectFields.map(sf => sf.Value)],
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
        dates: [],
        selectFields,
        experienceTypes: [
          {
            Value: 'NoExperience', Text: 'Нет опыта',
          },
          {
            Value: 'Between3And6', Text: 'От 3 до 6 лет',
          },
          {
            Value: 'Between1And3', Text: 'От 1 года до 3 лет',
          },
          {
            Value: 'MoreThan6', Text: 'Более 6 лет',
          },
        ],
        employmentTypes: [
          {
            Value: 'Full', Text: 'Полная занятость',
          },
          {
            Value: 'Part', Text: 'Частичная занятость',
          },
          {
            Value: 'Project', Text: 'Проектная работа',
          },
          {
            Value: 'Volunteer', Text: 'Волонтерство',
          },
          {
            Value: 'Probation', Text: 'Стажировка',
          },
        ],
        scheduleTypes: [
          {
            Value: 'FullDay', Text: 'Полный день',
          },
          {
            Value: 'Shift', Text: 'Сменный график',
          },
          {
            Value: 'Flexible', Text: 'Гибкий график',
          },
          {
            Value: 'Remote', Text: 'Удаленная работа',
          },
          {
            Value: 'FlyInFlyOut', Text: 'Вахтовый метод',
          },
        ],
        loadingCurrency: false,
        loadingSpecializations: false,
        currencies: [
          { Code: 'RUR', Name: 'Рубли' },
        ],
        specializations: [],
      }
    },
    methods: {
      searchCurrency (query) {
        if (query.match(/^\s*$/)) {
          return
        }
        let debounceTimer
        clearTimeout(debounceTimer)
        this.loadingCurrency = true
        debounceTimer = setTimeout(() => {
          HTTP.get('/api/currency/getByQuery', { params: { query } })
            .then(response => {
              this.currencies = response.data
            })
            .finally(() => {
              this.loadingCurrency = false
            })
        }, 250)
      },
      searchSpecializations (query) {
        if (query.match(/^\s*$/)) {
          return
        }
        let debounceTimer
        clearTimeout(debounceTimer)
        this.loadingSpecializations = true
        debounceTimer = setTimeout(() => {
          HTTP.get('/api/professionalArea/getByQuery', { params: { query } })
            .then(response => {
              this.specializations = response.data
            })
            .finally(() => {
              this.loadingSpecializations = false
            })
        }, 250)
      },
      onSubmit () {
        this.$emit('update:filter', { ...this.filter })
      },
      onDateChange (dates) {
        if (!dates) {
          this.filter.DateFrom = null
          this.filter.DateTo = null
        }
        this.filter.DateFrom = dates[0]
        this.filter.DateTo = dates[1]
      },
    },
  }
</script>

<style>
  .el-select {
    width: 100%;
  }

  .input-with-select .el-input-group__prepend {
    background-color: #fff;
  }

  .el-input-group__append, .el-input-group__prepend {
    width: 175px !important;
  }
</style>
