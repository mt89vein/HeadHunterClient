<script>
  import { HTTP } from '../../shared/http'

  export default {
    name: 'FetchData',
    props: {
      url: {
        type: String,
        required: true,
      },
      filter: {
        type: Object,
        default: () => {},
      },
    },
    data () {
      return {
        json: null,
      }
    },
    watch: {
      filter: {
        immediate: true,
        handler: 'fetch',
      },
    },
    methods: {
      fetch () {
        this.json = null
        HTTP.get(this.url, this.buildParams())
          .then(response => {
            this.json = response.data
          })
      },
      buildParams () {
        return {
          params: this.filter,
        }
      },
    },
    render (h) {
      return h('div', [
        this.$scopedSlots.default({
          json: this.json,
        }),
      ])
    },
  }
</script>
