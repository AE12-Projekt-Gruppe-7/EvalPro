<script setup lang="ts">

import {KriteriumServie} from "@/Services/KriteriumServie.ts";

const service = new KriteriumServie();

async function getKriterium(){
  await service.getKriteriums();
}

</script>

<template>
  <Button @click="getKriterium">
    Click Here
  </Button>
</template>

<style scoped></style>
