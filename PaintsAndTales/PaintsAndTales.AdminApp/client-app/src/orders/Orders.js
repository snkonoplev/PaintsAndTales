import axios from 'axios';

export default {
	name: 'Orders',
	data: function () {
		return {
			orders: []
		}
	},
	mounted: function () {
		axios.get('api/orders')
			.then(a => this.orders = a.data);
	},
	methods: {
		formatDate: function(date) {
			return this.$moment.utc(date).local().format('LLL');
		}
	}
}