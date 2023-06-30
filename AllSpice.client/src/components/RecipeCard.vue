<template>

<section class="container-fluid" v-if="recipes"
>
  <article class="card card-custom">
		<img :src="recipes.img" class="card-img-top recipe-img" alt="...">
		<div class="card-body">
			<h5 class="card-title">{{ recipes.title }}</h5>
			<p class="card-text">{{ recipes.description }}</p>
		</div>
		<div class="card-footer">
			<small class="text-muted">By {{ recipes.image }}</small>
			<button class="btn btn-danger btn-sm float-right" @click="deleteRecipe">Delete</button>
		</div>
	</article>
</section>

</template>

<script>

import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { Recipe } from "../models/Recipe.js";

export default {

	props: 
  { recipes: 
    { type: Recipe,
       required: true
    }
  },

	setup(props) {
    
		return {

			async deleteRecipe() {
				try {
					await recipesService.deleteRecipe(props.recipes.id)
					Pop.toast(`You have deleted ${props.recipes.title}`, 'success')
				} catch (error) {
					logger.error(error)
					Pop.toast(error.message, 'error')
				}
			},
		}
	}
}

</script>

<style scoped lang="scss">

.recipe-img {
  aspect-ratio: 1/.5;
  box-shadow: 0 0 15px 15px #000000;
  filter: drop-shadow(0 0 15px 15px #000000);
}

.card-custom {
  color: #efefef;
  text-shadow: 0 1px 5px #998ce2;
  overflow: hidden;
  min-height: 450px;
  background-color: #0a0b14f3;
  box-shadow: 0 0 15px 2px #0a0a0a4d;
  transition: .5 ease-in-out;
}

.card-custom-img {
  height: 200px;
  min-height: 200px;
  background-color: #00000090;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  opacity: .8;
  filter: brightness(1.3);
  box-shadow: 0 10px 0 10px #0a0a0a4d;
  border-color: inherit;
}

/* First border-left-width setting is a fallback */
.card-custom-img::after {
  position: absolute;
  content: '';
  top: 161px;
  left: 0;
  width: 0;
  height: 0;
  border-style: solid;
  border-top-width: 40px;
  border-right-width: 0;
  border-bottom-width: 0;
  border-left-width: 545px;
  border-left-width: calc(575px - 5vw);
  border-top-color: transparent;
  border-right-color: transparent;
  border-bottom-color: transparent;
  border-left-color: #0b0d12ef;
}

.card-custom-chef img {
  border-radius: 50%;
  box-shadow: 0 0 15px 3px #0a0a0a9a;
  position: absolute;
  border: 2.5px solid #000000;
  top: 100px;
  left: 1.25rem;
  width: 100px;
  height: 100px;
}


</style>