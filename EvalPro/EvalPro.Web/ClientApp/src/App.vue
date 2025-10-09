<script setup lang="ts">
import { ref, onMounted } from 'vue'

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Dialog from 'primevue/dialog'
import ColumnGroup from 'primevue/columngroup'
import Row from 'primevue/row'

import Button from 'primevue/button'

onMounted(() => {
  students.value = [
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

const students = ref()
const selectedStudents = ref()

const create_visible = ref(false)

function editStudent(id: any) {
  console.log('test')
}

function deleteStudent() {
  this.selectedStudents.forEach((value, index) => {
      this.students = this.students.filter(item => item.id !== value.id)
    
  })
}

function deselectRows() {
  this.selectedStudents = [];
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
      @click="deleteStudent()"
      :disabled="!selectedStudents || selectedStudents.length === 0"
    />

    <Button
      icon="pi pi-ban"
      label="Auswahl zurücksetzen"
      style="background-color: darkblue; border-color: darkblue"
      :disabled="!selectedStudents || selectedStudents.length === 0"
      @click="deselectRows()"
    />

    <Button icon="pi pi-plus" label="Neuer Eintrag" @click="create_visible = true" />
    <Dialog v-model:visible="create_visible" modal header="Edit Profile" :style="{ width: '25rem' }">
            
            <div class="flex justify-end gap-2">
                <Button type="button" label="Cancel" severity="secondary" @click="visible = false"></Button>
                <Button type="button" label="Save" @click="visible = false"></Button>
            </div>
        </Dialog>
  </div>

  <div class="main-card">
    <div class="card">
      <!-- Schülerliste -->
      <DataTable
        class="table"
        v-model:selection="selectedStudents"
        :value="students"
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
            <Button icon="pi pi-pencil" style="color: white" @click="editStudent(slotProps.data.id)" />
          </template>
        </Column>
        <Column style="width: 10%">
          <template #body="slotProps">
            <Button
              icon="pi pi-trash"
              @click="deleteStudent(slotProps.data.id)"
              style="background-color: red; border-color: red; color: white"
            />
          </template>
        </Column>
      </DataTable>
    </div>
  </div>
</template>

<style scoped>

</style>
