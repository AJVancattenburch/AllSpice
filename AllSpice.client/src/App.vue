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
import { computed, watchEffect } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { Offcanvas } from 'bootstrap'
import Pop from './utils/Pop'
import { logger } from "./utils/Logger"

export default {
  setup() {

    async function openSearchRecipesOffcanvas() {
      try {
        await Offcanvas.getOrCreateInstance('#myOffcanvas').show()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    }

    watchEffect(() => {
      if (AppState.query) {
        openSearchRecipesOffcanvas()
      }
    })

    return {
      openSearchRecipesOffcanvas,
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
