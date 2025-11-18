<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Dialog from 'primevue/dialog'
import ColumnGroup from 'primevue/columngroup'
import Row from 'primevue/row'
import Menu from 'primevue/menu';
import InputText from 'primevue/inputtext';
import Checkbox from 'primevue/checkbox';
import Button from 'primevue/button'
import Dropdown from 'primevue/dropdown';


const name1 = ref('');
const name2 = ref('');

const students = ref();
const selectedStudents = ref();

const visible = ref(false);
const visible_edit = ref(false);
const visible_rating = ref(false);


type Option = 'option1' | 'option2';

const selected = ref<Option>('option1');

// Checkbox Logik für Prüfling anlegen oder Prüfungsausschuss anlegen
const option1Checked = computed({
  get: () => selected.value === 'option1',
  set: (val: boolean) => {
    if (val) {
      selected.value = 'option1';
    }

  }
});

const option2Checked = computed({
  get: () => selected.value === 'option2',
  set: (val: boolean) => {
    if (val) {
      selected.value = 'option2';
    }
  }
});

const option3Checked = computed({
  get: () => selected.value === 'option3',
  set: (val: boolean) => {
    if (val) {
      selected.value = 'option3';
    }
  }
});

const selectedQuestion = ref();
const questions = ref([
    { name: 'TESTFRAGE', points: '10', comment: 'blablabla' },
]);

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


function addStudent(name1: string, name2: string) {
  students.value.push({id: Number(students.value[students.value.length - 1].id) + 1, name: name1, last_name: name2, work: 'test'});
}

function editStudent(id: any) {

}

function deleteStudent() {
  this.selectedStudents.forEach((value, index) => {
      this.students = this.students.filter(item => item.id !== value.id)
    
  })
}

function deselectRows() {
  this.selectedStudents = [];
}

const items = ref([
    {
        label: 'Prüfungsauschuss',
        items: [
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
            {
                label: 'Test',
                icon: 'pi pi-clock'
            },
        ]
    },
    
]);

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

    <h1 class="header-text">EvalPro</h1>
  </div>

  <div class="button-row">
    <!-- Button Reihe (Ausgewählte löschen, Auswahl zurücksetzen, Neuer Eintrag) -->
    <Button
      icon="pi pi-trash"
      label="Ausgewählte löschen"
      style="background-color: #e30013; border-color: #e30013"
      @click="deleteStudent()"
      :disabled="!selectedStudents || selectedStudents.length === 0"
    />

    <Button
      icon="pi pi-ban"
      label="Auswahl zurücksetzen"
      style="background-color: #e30013; border-color: #e30013"
      :disabled="!selectedStudents || selectedStudents.length === 0"
      @click="deselectRows()"
    />

     <!-- Popup Menu für "Neuer Eintrag" -->

    <Button icon="pi pi-plus" style="background-color: #e30013; border-color: #e30013;" label="Neuer Eintrag" @click="visible = true" />
    <Dialog v-model:visible="visible" modal header="Neuer Eintrag" :style="{ width: '35rem' }">

      <div class="checkbox-row">
        <div>
<Checkbox v-model="option1Checked"
        :binary="true"
        :disabled="option1Checked"
        />
                        <label for="c_student"> Prüfling anlegen </label>
        </div>
            <div>
<Checkbox v-model="option2Checked"
        :binary="true"
        :disabled="option2Checked"
        />
                        <label for="c_committee"> Prüflingsausschuss anlegen </label>
            </div>
            <div>
<Checkbox v-model="option3Checked"
        :binary="true"
        :disabled="option3Checked"
        />
                        <label for="c_committee"> Fragen anlegen / bearbeiten </label>
            </div>
            
      </div>
            
<div class="formgrid grid" v-if="option1Checked">
    <div class="field col-12 md:col-6" >
        <label for="firstname6">Vorname</label>
        <InputText v-model="name1" :invalid="!name1" class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
    </div>
    <div class="field col-12 md:col-6" >
        <label for="lastname6">Nachname</label>
        <InputText v-model="name2" :invalid="!name2" class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
    </div>
    <div class="field col-12">
        <label for="company">Ausbildungsbetrieb</label>
        <InputText id="company" type="text"  class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
    </div>
    <div class="field col-12 md:col-6">
        <label for="city">Ansprechpartner</label>
        <InputText id="city" type="text" class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
    </div>
    <div class="field col-12 md:col-6">
        <label for="city">Thema des Projekts</label>
        <InputText id="city" type="text" class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
    </div>
</div>

<div class="formgrid grid" v-if="option2Checked">
              <div class="field col-12 md:col-6" >
                  <label for="firstname6">Bezeichnung</label>
                  <InputText  class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
              <div class="field col-12 md:col-6" >
                  <label for="lastname6">Prüfungstage</label>
                  <InputText  class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
                  <div class="field col-12">
                  <label for="company">Ausbildungsberuf</label>
                  <InputText id="company" type="text"  class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
              </div>
          </div>

<div class="formgrid grid" v-if="option3Checked">
          
              <div class="field col-12">
                  <label for="firstname6">Alle Fragen</label>
                  <Dropdown v-model="selectedQuestion" class="text-base surface-overlay w-full" :options="questions" optionLabel="name" placeholder="Frage auswählen" />

              </div>
              <div class="field col-12" >
                  <label for="firstname6">Frage</label>
                  <InputText  class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
              <div class="field col-12 md:col-2" >
                  <label for="lastname6">Punkte</label>
                  <InputText  class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
                  <div class="field col-12 md:col-10">
                  <label for="company">Kommentar</label>
                  <InputText id="company" type="text"  class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
              </div>
</div>
      
            <div class="flex justify-end gap-2" >
                <Button type="button" label="Abbrechen" style="background-color: #e30013; border-color:#e30013" @click="visible = false"></Button>
                <Button type="button" v-if="!option3Checked" :disabled="!name1 || !name2" label="Speichern" style="background-color: #e30013; border-color:#e30013" @click="visible = false; addStudent(name1, name2)"></Button>
                <Button type="button" v-if="option3Checked"  label="Frage anlegen" style="background-color: #e30013; border-color:#e30013" @click="visible = false; addStudent(name1, name2)"></Button>
            </div>
        </Dialog>

      <Dialog v-model:visible="visible_edit" modal header="Prüfling bearbeiten" :style="{ width: '25rem' }"  >

          <div class="formgrid grid">
              <div class="field col-12 md:col-6" >
                  <label for="firstname6">Vorname</label>
                  <InputText v-model="name1" :invalid="!name1" class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
              <div class="field col-12 md:col-6" >
                  <label for="lastname6">Nachname</label>
                  <InputText v-model="name2" :invalid="!name2" class="text-base text-color surface-overlay p-2  border-round appearance-none  focus:border-primary w-full"/>
              </div>
              <div class="field col-12">
                  <label for="company">Ausbildungsbetrieb</label>
                  <InputText id="company" type="text"  class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
              </div>
              <div class="field col-12 md:col-6">
                  <label for="city">Ansprechpartner</label>
                  <InputText id="city" type="text" class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
              </div>
              <div class="field col-12 md:col-6">
                  <label for="city">Thema des Projekts</label>
                  <InputText id="city" type="text" class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"/>
              </div>
          </div>

          <div class="flex justify-end gap-2">
                <Button type="button" label="Abbrechen" style="background-color: #e30013; border-color:#e30013" @click="visible = false"></Button>
                <Button type="button" :disabled="!name1 || !name2" label="Speichern" style="background-color: #e30013; border-color:#e30013" @click="visible = false"></Button>
          </div>

        </Dialog>

        <Dialog v-model:visible="visible_rating" modal   :header="`Bewertung für ${name1} ${name2}`">

        <div class="card flex justify-center flex-wrap gap-4">
            <Button label="Projektdokumentation" style="background-color: #e30013; border: none" rounded />
            <Button label="Projektpräsentation"  style="background-color: #e30013; border: none" rounded />
            <Button label="Fachgespräch"  style="background-color: #e30013; border: none" rounded />
        </div>

        </Dialog>
  </div>

  <div class="main-card">


    <div class="card">

                  <Menu :model="items" class="p-menu" />


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
            <Button icon="pi pi-pencil" style="color: white; background-color: #e30013; border-color: #e30013" @click="visible_edit = true; editStudent(slotProps.data.id)" />
          </template>
        </Column>
        <Column style="width: 5%">
          <template #body="slotProps">
            <Button icon="pi pi-chart-bar" style="color: white; background-color: #e30013; border-color: #e30013" @click="visible_rating = true; name1 = slotProps.data.name; name2 = slotProps.data.last_name" />
          </template>
        </Column>
        <Column style="width: 10%">
          <template #body="slotProps">
            <Button
              icon="pi pi-trash"
              @click="deleteStudent(slotProps.data.id)"
              style="background-color: #e30013; border-color: #e30013; color: white"
            />
          </template>
        </Column>
      </DataTable>
    </div>
  </div>
</template>

<style scoped>

</style>
