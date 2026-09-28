package com.example.crustycrab

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.widget.LinearLayout
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.crustycrab.api.dto.CartDto
import com.example.crustycrab.api.dto.MenuItemDto
import com.example.crustycrab.databinding.ActivityMenuBinding
import com.example.crustycrab.databinding.ItemMenuItemBinding
import com.example.crustycrab.repository.CrustyRepository
import com.example.crustycrab.session.SessionManager
import com.example.crustycrab.util.ApiResult
import kotlinx.coroutines.launch

class MenuActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMenuBinding
    private var allItems: List<MenuItemDto> = emptyList()
    private var currentFilter: String = "All"
    private var cart: CartDto? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMenuBinding.inflate(layoutInflater)
        setContentView(binding.root)

        wireFilters()
        setupBottomNavigation()

        loadMenu()
        loadCart()
    }

    override fun onResume() {
        super.onResume()
        loadCart()
    }

    // =====================================================
    // FILTER CHIPS
    // =====================================================

    private fun wireFilters() {
        binding.chipAll.setOnClickListener { selectFilter("All") }
        binding.chipBoilBags.setOnClickListener { selectFilter("Boil Bags") }
        binding.chipStarters.setOnClickListener { selectFilter("Starters") }
    }

    private fun selectFilter(category: String) {
        currentFilter = category

        val selectedBg = R.drawable.bg_chip_selected
        val unselectedBg = R.drawable.bg_chip_unselected
        val selectedText = 0xFFFFFFFF.toInt()
        val unselectedText = 0xFF8A8F9E.toInt()

        listOf(
            binding.chipAll to "All",
            binding.chipBoilBags to "Boil Bags",
            binding.chipStarters to "Starters"
        ).forEach { (chip, cat) ->
            val selected = cat == category
            chip.setBackgroundResource(if (selected) selectedBg else unselectedBg)
            chip.setTextColor(if (selected) selectedText else unselectedText)
        }

        renderItems()
    }

    // =====================================================
    // MENU LOADING
    // =====================================================

    private fun loadMenu() {
        lifecycleScope.launch {
            when (val r = CrustyRepository.getMenu()) {
                is ApiResult.Success -> {
                    // Only show items that are currently available
                    allItems = r.data.filter { it.availability }
                    renderItems()
                }
                is ApiResult.Error -> toast(r.message)
            }
        }
    }

    private fun renderItems() {
        val container = binding.menuContainer
        container.removeAllViews()

        val filtered = if (currentFilter == "All") {
            allItems
        } else {
            allItems.filter { it.category.equals(currentFilter, ignoreCase = true) }
        }

        if (filtered.isEmpty()) {
            val tv = android.widget.TextView(this).apply {
                text = "No items in this category."
                setTextColor(0xFF8A8F9E.toInt())
                textSize = 14f
                gravity = android.view.Gravity.CENTER
                setPadding(0, 64, 0, 64)
            }
            container.addView(tv)
            return
        }

        val inflater = LayoutInflater.from(this)

        // Render items in rows of 2
        filtered.chunked(2).forEach { rowItems ->
            val rowView = LinearLayout(this).apply {
                orientation = LinearLayout.HORIZONTAL
                layoutParams = LinearLayout.LayoutParams(
                    LinearLayout.LayoutParams.MATCH_PARENT,
                    LinearLayout.LayoutParams.WRAP_CONTENT
                ).apply {
                    bottomMargin = (24 * resources.displayMetrics.density).toInt()
                }
            }

            rowItems.forEach { item ->
                val card = ItemMenuItemBinding.inflate(inflater, rowView, false)
                card.tvItemName.text = item.itemName
                card.tvItemDesc.text = item.description ?: ""
                card.tvItemPrice.text = getString(R.string.price_format, item.price.toInt())
                card.btnAdd.setOnClickListener { addItemToCart(item) }

                val lp = LinearLayout.LayoutParams(
                    0,
                    LinearLayout.LayoutParams.WRAP_CONTENT,
                    1f
                ).apply {
                    val margin = (8 * resources.displayMetrics.density).toInt()
                    setMargins(margin, 0, margin, 0)
                }
                card.root.layoutParams = lp
                rowView.addView(card.root)
            }

            // If odd number of items, add an empty spacer for balance
            if (rowItems.size == 1) {
                val spacer = View(this)
                spacer.layoutParams = LinearLayout.LayoutParams(0, 1, 1f)
                rowView.addView(spacer)
            }

            container.addView(rowView)
        }
    }

    // =====================================================
    // CART
    // =====================================================

    private fun loadCart() {
        val uid = SessionManager.getUserId(this)
        if (uid <= 0) return

        lifecycleScope.launch {
            val r = CrustyRepository.getCart(uid)
            if (r is ApiResult.Success) {
                cart = r.data
                updateCartButton()
            } else {
                // No cart yet — create one silently
                val created = CrustyRepository.createCart(uid)
                if (created is ApiResult.Success) {
                    cart = created.data
                    updateCartButton()
                }
            }
        }
    }

    private fun addItemToCart(item: MenuItemDto) {
        val uid = SessionManager.getUserId(this)
        if (uid <= 0) {
            toast(getString(R.string.login_required))
            return
        }

        val current = cart
        if (current == null) {
            // Cart not loaded yet — create it and retry
            lifecycleScope.launch {
                val created = CrustyRepository.createCart(uid)
                if (created is ApiResult.Success) {
                    cart = created.data
                    addItemToCart(item)
                } else {
                    toast(getString(R.string.cart_not_ready))
                }
            }
            return
        }

        lifecycleScope.launch {
            when (val r = CrustyRepository.addToCart(current.cartId, item.menuItemId, 1)) {
                is ApiResult.Success -> {
                    cart = r.data
                    updateCartButton()
                    toast("${item.itemName} added to cart")
                }
                is ApiResult.Error -> toast(r.message)
            }
        }
    }

    private fun updateCartButton() {
        val count = cart?.items?.sumOf { it.quantity } ?: 0
        binding.btnCart.text = getString(R.string.cart_label, count)
        binding.btnCart.setOnClickListener {
            startActivity(Intent(this, CartActivity::class.java))
        }
    }

    // =====================================================
    // BOTTOM NAVIGATION
    // =====================================================

    @Suppress("DEPRECATION")
    private fun setupBottomNavigation() {
        val nav = binding.bottomNavWrapper

        nav.getChildAt(0).setOnClickListener {
            startActivity(Intent(this, HomeActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(1).setOnClickListener {
            // Already on Menu — no-op
        }
        nav.getChildAt(2).setOnClickListener {
            startActivity(Intent(this, CartActivity::class.java))
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(3).setOnClickListener {
            startActivity(Intent(this, TrackerActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(4).setOnClickListener {
            startActivity(Intent(this, AccountActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
    }

    private fun toast(m: String) =
        Toast.makeText(this, m, Toast.LENGTH_SHORT).show()
}