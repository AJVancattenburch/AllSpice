import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Recipe } from '../models/Recipe'
import Pop from "../utils/Pop.js"
// import { Modal } from "bootstrap"

class RecipesService {

  async getAllRecipes() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
    logger.log('[ALL RECIPES] res.data:', res.data)
    logger.log(AppState.recipes)
  }

  async getRecipeById(recipeId) {
    AppState.activeRecipe = AppState.recipes.find(r => r.id == recipeId)
    logger.log('[ACTIVE RECIPE]', AppState.activeRecipe)
  }

  async createRecipe(recipeData) {
    logger.log('[GRABBING POST DATA => CREATED RECIPE]')
    const res = await api.post('api/recipes', recipeData)
    logger.log(res.data, `${recipeData}`)
    AppState.recipes.push(new Recipe(res.data))
    Pop.success(`${res.data.title} has been created!`)
  }

  async deleteRecipe(recipeId) {
    await api.delete(`api/recipes/${recipeId}`)
    AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
  }

  async editInstructions(steps) {
    const res = await api.put(`api/recipes/${AppState.activeRecipe.id}`, steps)
    AppState.activeRecipe.instructions = res.data.instructions
    logger.log('[EDITED STEPS] =>', res.data)
    Pop.toast('Recipe Updated!', 'success')
  }

  async searchRecipes(searchTerm) {
    logger.log('[SEARCHING FOR RECIPES CONTAINING THE WORD(S)] =>', searchTerm)
    const res = await api.get(`api/recipes?query=${searchTerm}`)
    AppState.recipes = res.data.map(r => new Recipe(r))
  }
}

export const recipesService = new RecipesService()