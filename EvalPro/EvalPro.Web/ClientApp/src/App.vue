<script setup lang="ts">
import { ref, onMounted } from 'vue';

import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import ColumnGroup from 'primevue/columngroup';
import Row from 'primevue/row';

const products = ref();
const selectedProduct = ref();

const visible = ref(false)
</script>

<template>

  <div class="header">
    <img alt="Vue logo" class="logo" src="/../ClientApp/src/regensburg_logo.webp" width="full" height="100"/>
  </div>
    <div class="main-card">
      <div class="card">
        <DataTable class="table" v-model:selection="selectedProduct" :value="products" selectionMode="single" dataKey="id" :metaKeySelection="false"
                @rowSelect="onRowSelect" @rowUnselect="onRowUnselect" >
            <Column field="student_id" header="ID"></Column>
            <Column field="student_name" header="Name"></Column>
            <Column field="student_work" header="Ausbildungsbetrieb"></Column>
        </DataTable>
        <Toast/>
      </div>

      <h1>test</h1>
    </div>
</template>

<style scoped>

  .header {
    display: flex;
    align-items: center;
    background-color: rgb(229, 229, 229);
  }

  .main-card {
    
  }

  .logo {
    margin-left: 5%;
    margin-bottom: 2rem;
   margin-top: 1rem;
  }

  .table {
    margin-top: 2rem;
    width: 40%;
  }
</style>
