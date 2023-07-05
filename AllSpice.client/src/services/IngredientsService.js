import { AppState } from '../AppState'
import { Ingredient } from '../models/Ingredient'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class IngredientsService {

  // async getAllIngredients() {
  //   const res = await api.get('api/ingredients')
  //   AppState.ingredients = res.data.map(i => new Ingredient(i))
  //   logger.log('[ALL INGREDIENTS] res.data:', res.data)
  //   logger.log(AppState.ingredients)
  // }

  async getIngredientByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.ingredients = res.data.map(i => new Ingredient(i))
  }

  async addIngredientToRecipe(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    AppState.ingredients.push(new Ingredient(res.data))
  }
}

export const ingredientsService = new IngredientsService()