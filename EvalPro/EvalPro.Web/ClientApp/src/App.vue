<script setup lang="ts">

import KriteriumService from "@/Services/KriteriumServie.ts";

const service = new KriteriumService();

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
