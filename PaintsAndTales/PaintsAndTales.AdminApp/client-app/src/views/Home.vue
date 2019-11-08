<template>
	<div class="container-fluid">
		<div class="row">
			<div class="col-3">
				<sales-chart></sales-chart>
			</div>
		</div>		
	</div>	
</template>
<script>
import SalesChart from '../components/SalesChart'

export default {
	components: {SalesChart}
}
</script>