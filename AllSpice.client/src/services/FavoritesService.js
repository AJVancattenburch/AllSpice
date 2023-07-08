import { AppState } from '../AppState'
import { FavoriteRecipe } from '../models/Favorite'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
class FavoritesService {

  async getMyFlavorIts() {
    const res = await api.get('account/favorites')
    AppState.flavorIts = res.data.map(f => new FavoriteRecipe(f))
    logger.log('[MY FAVORITE RECIPES] =>', res.data)
  }

  async addFlavorIt() {
    if (!AppState.account.id) {
      Pop.toast(`Log in to add this recipe to your favorites!`, 'error')
    } else {
      const relatedId = AppState.activeRecipe.id
      AppState.activeRecipe.value = { ...AppState.activeRecipe.value, relatedId }
      const res = await api.post('api/favorites', { relatedId })
      const newFlavorIt = new FavoriteRecipe(AppState.activeRecipe)
      const foundFlavorIt = res.data.id
      newFlavorIt.id = foundFlavorIt
      AppState.flavorIts.push(newFlavorIt)
      logger.log('[NEW FAVORITE RECIPE] =>', res.data)
    }
  }

  async removeFlavorIt(favoriteId) {
    await api.delete(`account/favorites/${favoriteId}`)
    const thisFlavorIt = AppState.flavorIts
    const flavorItToRemove = thisFlavorIt.findIndex(f => f.id === favoriteId)
    thisFlavorIt.splice(flavorItToRemove, 1)
  }
}

export const favoritesService = new FavoritesService()