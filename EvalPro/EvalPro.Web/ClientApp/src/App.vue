<script setup lang="ts">
import { ref, onMounted } from 'vue'

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import ColumnGroup from 'primevue/columngroup'
import Row from 'primevue/row'

import Button from 'primevue/button'

onMounted(() => {
  products.value = [
    //Schüler für die Liste
    { id: '1', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '3', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '4', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '5', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '6', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '7', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '8', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '9', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '10', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '11', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '12', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '13', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '14', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '15', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
    { id: '16', name: 'Paul', last_name: 'Zindler', work: 'x-plizit' },
  ]
})

const products = ref()
const selectedProduct = ref()

function editStudent(id: any) {
  console.log('test')
}

const visible = ref(false)
</script>

<template>
  <div class="header">
    <!-- Header mit Logo -->
    <img
      alt="Vue logo"
      class="logo"
      src="/../ClientApp/src/regensburg_logo.webp"
      width="full"
      height="100"
    />

    <h1 class="header-text">EvalPro Tool</h1>
  </div>

  <div class="button-row">
    <!-- Button Reihe (Ausgewählte löschen, Auswahl zurücksetzen, Neuer Eintrag) -->
    <Button
      icon="pi pi-trash"
      label="Ausgewählte löschen"
      style="background-color: red; border-color: red"
      disabled
    />

    <Button
      icon="pi pi-ban"
      label="Auswahl zurücksetzen"
      style="background-color: darkblue; border-color: darkblue"
      disabled
    />

    <Button icon="pi pi-plus" label="Neuer Eintrag" />
  </div>

  <div class="main-card">
    <div class="card">
      <!-- Schülerliste -->
      <DataTable
        class="table"
        v-model:selection="selectedProduct"
        :value="products"
        selectionMode="multiple"
        dataKey="id"
        :metaKeySelection="false"
        scrollable
        scrollHeight="800px"
      >
        <Column field="id" header="ID"></Column>
        <Column field="name" header="Vorname"></Column>
        <Column field="last_name" header="Nachname"></Column>
        <Column field="work" header="Ausbildungsbetrieb"></Column>
        <Column style="width: 5%">
          <template #body="slotProps">
            <Button icon="pi pi-pencil" @click="editStudent(slotProps.data.id)" />
          </template>
        </Column>
        <Column style="width: 10%">
          <template #body="slotProps">
            <Button
              icon="pi pi-trash"
              @click="deleteStudent(slotProps.data.id)"
              style="background-color: red; border-color: red"
            />
          </template>
        </Column>
      </DataTable>
      <Toast />
    </div>
  </div>
</template>

<style scoped></style>
