package com.example.crustycrab

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.crustycrab.databinding.ActivityTrackerBinding
import com.example.crustycrab.databinding.ItemOrderBinding
import com.example.crustycrab.repository.CrustyRepository
import com.example.crustycrab.session.SessionManager
import com.example.crustycrab.util.ApiResult
import kotlinx.coroutines.launch

class TrackerActivity : AppCompatActivity() {

    private lateinit var binding: ActivityTrackerBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityTrackerBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnBack.setOnClickListener { finish() }
        binding.swipeRefresh.setOnRefreshListener { loadOrders() }

        setupBottomNavigation()
        loadOrders()
    }

    override fun onResume() {
        super.onResume()
        loadOrders()
    }

    private fun loadOrders() {
        val uid = SessionManager.getUserId(this)
        if (uid <= 0) {
            toast(getString(R.string.login_required))
            return
        }

        binding.swipeRefresh.isRefreshing = true
        binding.llOrdersContainer.removeAllViews()

        lifecycleScope.launch {
            when (val r = CrustyRepository.getOrders(uid)) {
                is ApiResult.Success -> {
                    binding.swipeRefresh.isRefreshing = false
                    if (r.data.isEmpty()) {
                        binding.tvEmpty.visibility = View.VISIBLE
                    } else {
                        binding.tvEmpty.visibility = View.GONE
                        val inflater = LayoutInflater.from(this@TrackerActivity)
                        r.data.forEach { order ->
                            val row = ItemOrderBinding.inflate(
                                inflater, binding.llOrdersContainer, false
                            )
                            row.tvOrderId.text =
                                getString(R.string.tracker_order_number, order.orderID)
                            row.tvStatus.text = order.orderStatus
                            row.tvDate.text = order.orderDate?.take(10) ?: ""
                            row.tvTotal.text =
                                getString(R.string.price_format, order.totalAmount.toInt())
                            row.tvItems.text = order.items.joinToString(", ") {
                                "${it.itemName} ${getString(R.string.qty_format, it.quantity)}"
                            }
                            binding.llOrdersContainer.addView(row.root)
                        }
                    }
                }
                is ApiResult.Error -> {
                    binding.swipeRefresh.isRefreshing = false
                    toast(r.message)
                }
            }
        }
    }

    @Suppress("DEPRECATION")
    private fun setupBottomNavigation() {
        val nav = binding.bottomNavWrapper

        nav.getChildAt(0).setOnClickListener {
            startActivity(Intent(this, HomeActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(1).setOnClickListener {
            startActivity(Intent(this, MenuActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(2).setOnClickListener {
            startActivity(Intent(this, CartActivity::class.java))
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(3).setOnClickListener {
            // Already on Tracker — no-op
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