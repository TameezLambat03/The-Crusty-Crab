
package com.example.crustycrab

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.widget.TextView
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

        binding.btnBack.setOnClickListener {
            finish()
        }

        loadOrders()
    }

    override fun onResume() {
        super.onResume()
        loadOrders()
    }

    private fun loadOrders() {

        val userId = SessionManager.getUserId(this)

        if (userId <= 0) {
            showEmptyMessage("Please log in to view your orders.")
            return
        }

        binding.llOrders.removeAllViews()

        lifecycleScope.launch {

            when (val result = CrustyRepository.getOrders(userId)) {

                is ApiResult.Success -> {

                    if (result.data.isEmpty()) {
                        showEmptyMessage("You have no orders yet.")
                        return@launch
                    }

                    val inflater = LayoutInflater.from(this@TrackerActivity)

                    result.data.forEach { order ->

                        val row = ItemOrderBinding.inflate(
                            inflater,
                            binding.llOrders,
                            false
                        )

                        row.tvOrderId.text =
                            getString(
                                R.string.tracker_order_number,
                                order.orderID
                            )

                        row.tvStatus.text = order.orderStatus

                        row.tvDate.text =
                            order.orderDate?.take(10) ?: ""

                        row.tvTotal.text =
                            getString(
                                R.string.price_format,
                                order.totalAmount.toInt()
                            )

                        row.tvItems.text =
                            order.items.joinToString(", ") {
                                "${it.itemName} ${
                                    getString(
                                        R.string.qty_format,
                                        it.quantity
                                    )
                                }"
                            }

                        binding.llOrders.addView(row.root)
                    }
                }

                is ApiResult.Error -> {
                    showEmptyMessage(result.message)
                }
            }
        }
    }

    private fun showEmptyMessage(message: String) {

        binding.llOrders.removeAllViews()

        val textView = TextView(this).apply {

            text = message

            setTextColor(0xFF8A8F9E.toInt())

            textSize = 15f

            gravity = android.view.Gravity.CENTER

            setPadding(
                16,
                48,
                16,
                48
            )
        }

        binding.llOrders.addView(textView)
    }
}

