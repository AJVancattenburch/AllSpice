import { AppState } from '../AppState'
import { Favorite, FavoriteRecipe } from '../models/Favorite'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
class FavoritesService {

  async getMyFlavorIts() {
    const res = await api.get('account/favorites')
    AppState.myFlavorIts = res.data.map(f => new FavoriteRecipe(f))
    logger.log('[MY FAVORITE RECIPES] =>', res.data)
  }

  async addFlavorIt(recipeId) {
    if (!AppState.account.id) {
      Pop.toast(`Log in to add this recipe to your favorites!`, 'error')
    } else {
      // const relatedId = AppState.activeRecipe.id
      // AppState.activeRecipe.value = { ...AppState.activeRecipe.value, relatedId }
      const res = await api.post('api/favorites', { recipeId })
      const newFlavorIt = new Favorite(res.data)
      // const foundFlavorIt = res.data.id
      // newFlavorIt.id = foundFlavorIt
      AppState.flavorIts.push(newFlavorIt)
      logger.log('[NEW FAVORITE RECIPE] =>', res.data)
    }
  }

  async removeFlavorIt(recipeId) {
    const res = await api.delete(`api/favorites/${recipeId}`)
    logger.log('[REMOVED FAVORITE RECIPE] =>', res.data)
    const thisFlavorIt = AppState.flavorIts
    const flavorItToRemove = thisFlavorIt.findIndex(f => f.id === recipeId)
    thisFlavorIt.splice(flavorItToRemove, 1)
    AppState.flavorIts = thisFlavorIt
  }
}

export const favoritesService = new FavoritesService()