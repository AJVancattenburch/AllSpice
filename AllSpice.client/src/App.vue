<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>

  <Offcanvas id="offcanvasWithBothOptions">
      <CreateRecipeForm />
  </Offcanvas>

  <OffCanvas  id="detailsOffcanvas" class="offcanvas offcanvas-xxl offcanvas-top">
    <RecipeDetailsCard />
  </OffCanvas>

  <div class="myOffcanvas">
    <SearchRecipesOffcanvas id="myOffcanvas" class="myOffcanvas" style="" />
  </div>

</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

.myOffcanvas {
  width: 100vw;
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
